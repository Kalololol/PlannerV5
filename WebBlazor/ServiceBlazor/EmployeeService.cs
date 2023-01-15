using Application.ModelDto;
using Application.Service.Command;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using WebBlazor.ModelWebBlazor;

namespace WebBlazor.ServiceBlazor
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            return await httpClient.GetFromJsonAsync<List<EmployeeModel>>("api/employee/allEmployees");
        }

        public async Task<EmployeeModel> GetEmployeeById(int id)
        {
            return await httpClient.GetFromJsonAsync<EmployeeModel>($"api/employee/getEmployeeById/{id}");
        }

        public async Task<EmployeeModel> CreateEmployee(EmployeeModel employee)
        {
            //var input = await httpClient.PostAsJsonAsync<EmployeeModel>("api/employee/createEmployee", employee);
            //https://github.com/GavinLonDigital/ShopOnlineSolution/blob/main/ShopOnline.Web/Services/ShoppingCartService.cs
            throw new NotImplementedException();

        }

        public Task<EmployeeModel> EditEmployee(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        
    }
}
