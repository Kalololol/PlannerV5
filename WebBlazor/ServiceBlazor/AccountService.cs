using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebBlazor.ModelWebBlazor;
using static System.Net.WebRequestMethods;

namespace WebBlazor.ServiceBlazor
{
    public interface IAccountService
    {
        string GenerateJwt(EmployeeModel employee, RoleModel role);
        Task<string> Login(LoginModel login);
    }

    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;

        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(HttpClient httpClient, AuthenticationSettings authenticationSettings)
        {
            this.httpClient = httpClient;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> Login(LoginModel login)
        {

            var response = await httpClient.PostAsJsonAsync("api/account/login", login);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Niepoprawne dane logowania");
            }
            return response.ToString();
        }

        public string GenerateJwt(EmployeeModel employee, RoleModel role)
        {
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
            var result = tokenHandler.WriteToken(token);

            return result;
        }
    }
}