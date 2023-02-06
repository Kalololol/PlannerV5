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
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; set; }

    public GetEmployeeByIdQuery(int id)
    {
        Id = id;
    }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IRepository<Employee> _employeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IRepository<Employee> employeRepository, IMapper mapper)
        {
            _employeRepository = employeRepository;
            _mapper = mapper;
        }

        public Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = _employeRepository.GetById(request.Id);
            var result = _mapper.Map<EmployeeDto>(employee);

            return Task.FromResult(result);
        }
    }
    }
