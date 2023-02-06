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
    public class GetRequestByIdQuery : IRequest<RequestDto>
    {
        public int Id { get; set; }

        public GetRequestByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, RequestDto>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IMapper _mapper;

        public GetRequestByIdQueryHandler(IRepository<Request> requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public Task<RequestDto> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var req = _requestRepository.GetById(request.Id);
            var result = _mapper.Map<RequestDto>(req);

            return Task.FromResult(result);
        }
    }
}
