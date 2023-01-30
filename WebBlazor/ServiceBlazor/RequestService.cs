using Domain;
using WebBlazor.ModelWebBlazor;

namespace WebBlazor.ServiceBlazor
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient httpClient;
        public RequestService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }                
        public async Task<List<RequestModel>> GetRequests()
        {
            return await httpClient.GetFromJsonAsync<List<RequestModel>>("api/request/allRequests");
        }
        public async Task<RequestModel> GetRequestById(int id)
        {
            return await httpClient.GetFromJsonAsync<RequestModel>($"api/request/getRequestById/{id}");
        }
/*        public async Task<RequestModel> GetAllRequestsByEmployee(int id)
        {
            return await httpClient.GetFromJsonAsync<List<RequestModel>>($"api/request/getAllRequestsByEmployee/{id}");

        }*/

        public async Task<RequestModel> AddRequest(RequestModel request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/request/addRequest", request);
                if (response.IsSuccessStatusCode)
                {
                    /*if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(EmployeeModel);
                    }*/
                    return request;
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
        public async Task<RequestModel> EditRequest(RequestModel request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/request/editRequest", request);
                if (response.IsSuccessStatusCode)
                {
                    /*if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(EmployeeModel);
                    }*/
                    return request;
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
    }
}
