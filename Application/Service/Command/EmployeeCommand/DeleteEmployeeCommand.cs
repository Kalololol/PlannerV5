using Data.Repositories;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Command
{
    public class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IRepository<Employee> _employeeRepository;

        public DeleteEmployeeCommandHandler(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employee = _employeeRepository.GetById(request.Id);

            if (employee != null)
            {
                _employeeRepository.Delete(employee);
            }

            return Task.FromResult(Unit.Value);
        }
    }

}
