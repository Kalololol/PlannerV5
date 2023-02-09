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
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AccountController(IAccountService accountService, 
            IMediator mediator, 
            IMapper mapper, 
            AuthenticationStateProvider authStateProvider, 
            ILocalStorageService localStorage)
        {
            _accountService = accountService;
            _mediator = mediator;
            _mapper = mapper;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel login)
        {
            try
            {
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

                await _localStorage.SetItemAsync("token", token);
                await _authStateProvider.GetAuthenticationStateAsync();

                //var result = await Http.PostAsJsonAsync("api/account/login", login);
                //var token = await result.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("token", token);

                return Ok(token);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Błąd podczas przetwarzania żądania autoryzacji.");
            }
        }


       


    }
}
