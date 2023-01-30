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
    public class GetAllRequestsByEmployeeQuery : IRequest<List<RequestDto>>
    {
        public int Id { get; set; }

        public GetAllRequestsByEmployeeQuery(int id)
        {
            Id = id;
        }
    }
    public class GetAllRequestsByEmployeeQueryHandler : IRequestHandler<GetAllRequestsByEmployeeQuery, List<RequestDto>>
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IMapper _mapper;

        public GetAllRequestsByEmployeeQueryHandler(IRepository<Request> requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<List<RequestDto>> Handle(GetAllRequestsByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var requests = _requestRepository.GetAll();
            List<RequestDto> result = new List<RequestDto>();

            foreach (var i in requests)
            {
                if (i.EmployeeId == request.Id)
                {
                    if (i.Active == true)
                    {
                        var req = _mapper.Map<RequestDto>(i);
                        result.Add(req);
                    }
                }
            }

            return result;
        }
    }
}
