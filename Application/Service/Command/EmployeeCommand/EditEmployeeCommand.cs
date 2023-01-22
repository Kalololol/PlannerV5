using Application.ModelDto;
using AutoMapper;
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
    public class EditEmployeeCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AddressEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public EditEmployeeCommand(int id, string name, string surname, string addressEmail, string phoneNumber, string licenseNumber, string password, bool active)
        {
            Id = id;
            Name = name;
            Surname = surname;
            AddressEmail = addressEmail;
            PhoneNumber = phoneNumber;
            LicenseNumber = licenseNumber;
            Password = password;
            Active = active;
        }
    }

    public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, Unit>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EditEmployeeCommandHandler(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.GetById(request.Id);

            if (employee != null)
            {
                employee.Name = request.Name;
                employee.Surname = request.Surname;
                employee.AddressEmail = request.AddressEmail;
                employee.PhoneNumber = request.PhoneNumber;
                employee.LicenseNumber = request.LicenseNumber;
                employee.Password = request.Password;
                employee.Active = request.Active;

            }

            _employeeRepository.Update(employee);


            return Task.FromResult(Unit.Value);
        }
    }

}
