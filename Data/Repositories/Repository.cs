using Data.Context;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly AppDbContext _context;
        private DbSet<TEntity> entities;

        public Repository(AppDbContext context)
        {
            this._context = context;
            this.entities = context.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return entities;
        }
        public TEntity GetById(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }
        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public void Update(TEntity entity)
        {
            entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(TEntity entity)
        {
            entities.Remove(entity);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
