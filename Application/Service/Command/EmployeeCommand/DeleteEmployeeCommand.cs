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
        private readonly IRepository<Request> _requestRepository;


        public DeleteEmployeeCommandHandler(IRepository<Employee> employeeRepository, IRepository<Request> requestRepository)
        {
            _employeeRepository = employeeRepository;
            _requestRepository = requestRepository;
        }

        public Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employee = _employeeRepository.GetById(request.Id);

            if (employee != null)
            {
                employee.Active = false;
                _employeeRepository.Update(employee);
            }

            var allRequest = _requestRepository.GetAll().ToList();
            foreach (var ar in allRequest)
            {
                if (ar.EmployeeId == request.Id)
                {
                    ar.Active = false;
                    _requestRepository.Update(ar);
                }
            }

            return Task.FromResult(Unit.Value);
        }
    }

}
