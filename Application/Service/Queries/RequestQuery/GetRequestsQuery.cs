using Application.ModelDto;
using AutoMapper;
using Data.Repositories;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Queries
{
    public class GetRequestsQuery : IRequest<List<RequestDto>>
    {
    }
    public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, List<RequestDto>>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IMapper _mapper;

        public GetRequestsQueryHandler(IRepository<Request> requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public Task<List<RequestDto>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = _requestRepository.GetAll();
            List<RequestDto> result = new List<RequestDto>();

            foreach (var i in requests)
            {
                if (i.Active == true)
                {
                    var req = _mapper.Map<RequestDto>(i);
                    result.Add(req);
                }
            }

            return Task.FromResult(result);
        }
    }
}
