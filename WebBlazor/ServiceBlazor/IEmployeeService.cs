using Application.ModelDto;

namespace WebBlazor.ServiceBlazor
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> CreateEmployee(EmployeeDto employee);
        Task<EmployeeDto> EditEmployee(EmployeeDto employee);

    }
}
