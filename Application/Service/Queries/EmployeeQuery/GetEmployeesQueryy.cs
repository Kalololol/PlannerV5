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
    public class GetEmployeesQuery : IRequest<List<EmployeeDto>>
    {
    }
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {

        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = _employeeRepository.GetAll();
            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var i in employees)
            {
                if (i.Active == true)
                {
                    var employee = _mapper.Map<EmployeeDto>(i);
                    result.Add(employee);
                }
            }

            return result;
        }
    }
}
