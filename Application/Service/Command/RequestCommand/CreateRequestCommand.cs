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
    public class CreateRequestCommand : IRequest
    {
        public DateTime DayIndisposition { get; set; }
        public string Change { get; set; } //D-Day, N-Night, A-AllDay
        public string TypeRequest { get; set; } // W-Work, H-Holiday
        public int EmployeeId { get; set; }

    }

    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, Unit>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IMapper _mapper;

        public CreateRequestCommandHandler(IRepository<Request> requestRepository, IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.GetById(1); // tutaj wrzucic employeeId zalogowanego usera
            var req = new RequestDto()
            {
                DayIndisposition = request.DayIndisposition,
                Change = request.Change,
                TypeRequest = request.TypeRequest,
                EmployeeName = employee.Surname + " " + employee.Name,   
                EmployeeId = 1, // tutaj wrzucic id zalogowanego usera
                Active = true
            };
            var result = _mapper.Map<Request>(req);
            _requestRepository.Add(result);

            return Task.FromResult(Unit.Value);
        }
    }
}
