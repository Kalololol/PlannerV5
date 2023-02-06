using Application.ModelDto;
using AutoMapper;
using Data.Repositories;
using Domain;
using MediatR;

namespace Application.Service.Queries
{
    public class AuthorizationEmployeeQuery : IRequest<EmployeeDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AddressEmail { get; set; }
        public string Password { get; set; }

        public AuthorizationEmployeeQuery(string name, string surname, string addressEmail, string password)
        {
            Name = name;
            Surname = surname;
            AddressEmail = addressEmail;
            Password = password;
        }
    }
    public class AuthorizationEmployeeQueryHandler : IRequestHandler<AuthorizationEmployeeQuery, EmployeeDto>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _roleRepository;

        private readonly IMapper _mapper;

        public AuthorizationEmployeeQueryHandler(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public Task<EmployeeDto> Handle(AuthorizationEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employees = _employeeRepository.GetAll();
            var employee = employees.FirstOrDefault(x => x.AddressEmail == request.AddressEmail );
            //if ( employee == null || employee.Active == false || employee.Password != request.Password)
            //{

            //}
            //else
            //{

            //}
            var role = _roleRepository.GetById(employee.RoleId);



            return Task.FromResult(_mapper.Map<EmployeeDto>(employee));

        }
    }


}
