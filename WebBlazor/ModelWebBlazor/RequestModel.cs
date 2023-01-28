using System.ComponentModel.DataAnnotations;

namespace WebBlazor.ModelWebBlazor
{
    public class RequestModel
    {
        public int Id { get; set; }
        public DateTime DayIndisposition { get; set; }
        public string Change { get; set; } //D-Day, N-Night, A-AllDay
        public string TypeRequest { get; set; } // W-Work, H-Holiday
        public int EmployeeId { get; set; }
       // public string EmployeeName { get; set; }
        public bool Active { get; set; }
        public RequestModel()
        {

        }
    }
}
