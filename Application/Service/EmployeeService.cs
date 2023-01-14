using Application.ModelDto;
using Application.Service.Queries;
using AutoMapper;
using Data.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public List<EmployeeDto> GetAll()
        {
            var employees = _employeeRepository.GetAll();
            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var i in employees)
            {
                var employee = _mapper.Map<EmployeeDto>(i);
                result.Add(employee);
            }

            return result;
        }
    }
}
