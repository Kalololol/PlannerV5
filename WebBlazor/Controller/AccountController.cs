using Application.Service.Queries;
using AutoMapper;
using Blazored.SessionStorage;
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
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ISessionStorageService _sessionStorageService;


        public AccountController(IAccountService accountService,
            IMediator mediator,
            IMapper mapper,
            AuthenticationStateProvider authStateProvider,
            ISessionStorageService sessionStorageService
            )
        {
            _accountService = accountService;
            _mediator = mediator;
            _mapper = mapper;
            _authStateProvider = authStateProvider;
            _sessionStorageService = sessionStorageService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel login)
        {
            try
            {
                IActionResult response = Unauthorized();

                var search = _mediator.Send(_mapper.Map<GetEmployeeByEmailQuery>(login));
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
                response = Ok(new { Token = token.ToString() });              

                return Ok(new { Token = token });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Błąd podczas przetwarzania żądania autoryzacji.");
            }
        }






    }
}
