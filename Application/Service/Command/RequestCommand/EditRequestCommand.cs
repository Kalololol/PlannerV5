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
    public class EditRequestCommand : IRequest
    {
        public int Id { get; set; }
        public DateTime DayIndisposition { get; set; }
        public char Change { get; set; } //D-Day, N-Night, A-AllDay
        public char TypeRequest { get; set; } // W-Work, H-Holiday
        public bool Active { get; set; }
    }

    public class EditRequestCommandHandler : IRequestHandler<EditRequestCommand, Unit>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IMapper _mapper;

        public EditRequestCommandHandler(IRepository<Request> requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(EditRequestCommand request, CancellationToken cancellationToken)
        {
            var req = _requestRepository.GetById(request.Id);

            if (req != null)
            {
                req.DayIndisposition = request.DayIndisposition;
                req.Change = request.Change;
                req.TypeRequest= request.TypeRequest;
                req.Active = request.Active;    
            }

            _requestRepository.Update(req);
            return Task.FromResult(Unit.Value);

        }
    }
}