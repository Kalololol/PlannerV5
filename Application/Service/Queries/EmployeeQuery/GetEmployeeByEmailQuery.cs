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

namespace Application.Service.Queries
{
    public class GetEmployeeByEmailQuery : IRequest<EmployeeDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public GetEmployeeByEmailQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public class GetEmployeeByEmailQueryHandler : IRequestHandler<GetEmployeeByEmailQuery, EmployeeDto>
    {
        private readonly IRepository<Employee> _employeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByEmailQueryHandler(IRepository<Employee> employeRepository, IMapper mapper)
        {
            _employeRepository = employeRepository;
            _mapper = mapper;
        }

        public Task<EmployeeDto> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken)
        {
            var employees = _employeRepository.GetAll();
            var search = employees.FirstOrDefault(e => e.AddressEmail == request.Email);
            var result = _mapper.Map<EmployeeDto>(search);

            return Task.FromResult(result);
        }
    }
}


