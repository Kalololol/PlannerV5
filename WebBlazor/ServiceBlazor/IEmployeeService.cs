using Application.ModelDto;
using WebBlazor.ModelWebBlazor;

namespace WebBlazor.ServiceBlazor
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetEmployees();
        Task<EmployeeModel> GetEmployeeById(int id);
        Task<EmployeeModel> CreateEmployee(EmployeeModel employee);
        Task<EmployeeModel> EditEmployee(EmployeeModel employee);

    }
}
