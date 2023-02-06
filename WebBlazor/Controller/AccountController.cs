using Application.ModelDto;
using Application.Service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebBlazor.Exceptions;
using WebBlazor.ModelWebBlazor;
using WebBlazor.ServiceBlazor;

namespace WebBlazor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMediator mediator, IMapper mapper)
        {
            _accountService = accountService;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<EmployeeModel>> Login(LoginModel login)
        {
            try
            {
                var search = _mediator.Send(_mapper.Map<GetEmployeeByEmailQuery>(login));
                //var searchs = _mediator.Send(new GetEmployeesQuery());
                //var input = _mapper.Map<List<EmployeeModel>>(searchs);
                //var employee = _mapper.Map<EmployeeModel>(input.FirstOrDefault(e => e.AddressEmail == login.Email));

               // var employee = _mapper.Map<EmployeeModel>(search);
                EmployeeModel employee = new EmployeeModel()
                {
                    Id = search.Result.Id,
                    Name = search.Result.Name,
                    Surname = search.Result.Surname,
                    AddressEmail = search.Result.AddressEmail,
                    LicenseNumber = search.Result.LicenseNumber,
                    PhoneNumber = search.Result.PhoneNumber,
                    RoleId = search.Result.RoleId,
                    Active = search.Result.Active,
                    Password= search.Result.Password

                };

                if (employee == null)
                {
                    throw new BadRequestException("Invalid username or password");
                }
                if (employee.Password != login.Password)
                {
                    throw new BadRequestException("Invalid username or password");

                }
                var searchRole = _mediator.Send(new GetRoleByIdQuery(employee.RoleId));
                RoleModel role = new RoleModel()
                {
                    Id= searchRole.Result.Id,
                    Name = searchRole.Result.Name
                };


                var token = _accountService.GenerateJwt(employee, role);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status501NotImplemented,
                    "Błąd podczas przetwarzania żądania autoryzacji.");
            }
        }
    }
}
