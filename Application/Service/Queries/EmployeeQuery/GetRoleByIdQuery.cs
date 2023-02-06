using Application.ModelDto;
using AutoMapper;
using Data.Repositories;
using Domain;
using MediatR;


namespace Application.Service.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleDto>
    {
        public int Id { get; set; }

        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = _roleRepository.GetById(request.Id);
            var result = _mapper.Map<RoleDto>(role);


            return Task.FromResult(result);
        }
    }
}


