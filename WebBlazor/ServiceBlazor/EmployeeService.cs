using Application.ModelDto;
using Application.Service.Command;
using AutoMapper;
using MediatR;
using System.Net.Http.Json;

namespace WebBlazor.ServiceBlazor
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EmployeeService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employee)
        {
                await _mediator.Send(_mapper.Map<CreateEmployeeCommand>(employee));
            return( employee );
        }

        public Task<EmployeeDto> EditEmployee(EmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeDto>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
