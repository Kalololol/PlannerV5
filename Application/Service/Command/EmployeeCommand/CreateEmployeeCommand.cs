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
    public class CreateEmployeeCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AddressEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string Password { get; set; }


        public CreateEmployeeCommand() { }

        public CreateEmployeeCommand(string name, string surname, string addressEmail, string phoneNumber, string licenseNumber, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.AddressEmail = addressEmail;
            this.PhoneNumber = phoneNumber;
            this.LicenseNumber = licenseNumber;
            this.Password = password;
        }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Unit>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employee = new EmployeeDto()
            {
                Name = request.Name,
                Surname = request.Surname,
                AddressEmail = request.AddressEmail,
                PhoneNumber = request.PhoneNumber,
                LicenseNumber = request.LicenseNumber,
                Password = request.Password,
                Active = true,
            };
            var result = _mapper.Map<Employee>(employee);
            _employeeRepository.Add(result);

            return Task.FromResult(Unit.Value);
        }
    }



}
