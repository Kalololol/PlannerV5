using Application.Service.Command;
using Application.Service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
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
       // private readonly ILocalStorageService _localStorage;

        public AccountService(HttpClient httpClient, 
            IMediator mediator, 
            IMapper mapper, 
            AuthenticationSettings authenticationSettings 
            //,ILocalStorageService localStorage
            )
        {
            this.httpClient = httpClient;
            _mediator = mediator;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
           // _localStorage = localStorage;
        }

        public async Task<LoginModel> Login(LoginModel login)
        {

            var response = await httpClient.PostAsJsonAsync("api/account/login", login);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to login");
            }

            return login;

            //try
            //{
            //    var response = await httpClient.PostAsJsonAsync("api/account/login", login);
            //    if (response.IsSuccessStatusCode)
            //    {
            //       // await _localStorage.SetItemAsync("token", response);
            //       // return login;
            //    }
            //    else
            //    {
            //        var message = await response.Content.ReadAsStringAsync();
            //        throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
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


            return tokenHandler.WriteToken(token);
        }            
    }
}