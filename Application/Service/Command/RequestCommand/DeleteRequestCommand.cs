using AutoMapper;
using Data.Repositories;
using Domain;
using MediatR;


namespace Application.Service.Command
{
    public class DeleteRequestCommand : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, Unit>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IMapper _mapper;

        public DeleteRequestCommandHandler(IRepository<Request> requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public Task<Unit> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            var req = _requestRepository.GetById(request.Id);

            if (req != null)
            {
                req.Active = false;
                _requestRepository.Update(req);
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
