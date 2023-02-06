using Application.Service.Command;
using Application.Service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using WebBlazor.Exceptions;
using WebBlazor.ModelWebBlazor;

namespace WebBlazor.ServiceBlazor
{
    public interface IAccountService
    {
        string GenerateJwt(EmployeeModel employee, RoleModel role);
        Task<LoginModel> Login(LoginModel login);

    }

    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(HttpClient httpClient, IMediator mediator, IMapper mapper, AuthenticationSettings authenticationSettings)
        {
            this.httpClient = httpClient;
            _mediator = mediator;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<LoginModel> Login(LoginModel login)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/account/login", login);

                if (response.IsSuccessStatusCode)
                {
                    return login;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateJwt(EmployeeModel employee, RoleModel role)
        {
            // var employees = _mapper.Map<List<EmployeeModel>>(_mediator.Send(new GetEmployeesQuery()));
            //  var employee = employees.Find(x => x.AddressEmail == dto.Email);
           /* var input =  _mediator.Send(_mapper.Map<GetEmployeeByEmailQuery>(dto));
            var employee = _mapper.Map<EmployeeModel>(input);
            if(employee == null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            if(employee.Password != dto.Password)
            {
                throw new BadRequestException("Invalid username or password");

            }
            var role = _mapper.Map<RoleModel>(_mediator.Send(new GetRoleByIdQuery(employee.RoleId)));
*/

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{employee.Name} {employee.Surname}"),
                new Claim(ClaimTypes.Role, $"{role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                    _authenticationSettings.JwtIssuer,
                    claims,
                    expires: expires,
                    signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }            
    }
}