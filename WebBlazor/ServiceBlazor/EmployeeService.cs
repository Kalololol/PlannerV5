using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
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
       
        public async Task<EmployeeModel> AddEmployee(EmployeeModel employee)
        {
            try
            {                                
                var response = await httpClient.PostAsJsonAsync("api/employee/addEmployee", employee);
                if (response.IsSuccessStatusCode)
                {
                    /*if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(EmployeeModel);
                    }*/
                    return employee;
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

        public async Task<EmployeeModel> EditEmployee(EmployeeModel employee)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/employee/editEmployee", employee);
                if (response.IsSuccessStatusCode)
                {
                    return employee;
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

        public async Task<EmployeeModel> DeleteEmployee(EmployeeModel employee)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/employee/deleteEmployee", employee);
                if (response.IsSuccessStatusCode)
                {
                    return employee;
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
/*        public async Task<string> Login(LoginModel login)
        {
            var response = await httpClient.PostAsJsonAsync<LoginModel>($"api/account/login", login);
            if (response.IsSuccessStatusCode)
            {
                return employee;
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
}*/
    }
}
