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

        public Task<EmployeeModel> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeModel> CreateEmployee(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeModel> EditEmployee(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        
    }
}
