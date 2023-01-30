using WebBlazor.ModelWebBlazor;

namespace WebBlazor.ServiceBlazor
{
    public interface IRequestService
    {
        Task<List<RequestModel>> GetRequests();
        Task<RequestModel> GetRequestById(int id);
     //   Task<RequestModel> GetAllRequestsByEmployee(int id);

        Task<RequestModel> AddRequest(RequestModel request);
        Task<RequestModel> EditRequest(RequestModel request);
        //Task<RequestModel> DeleteEmployee(EmployeeModel employee);
    }
}
