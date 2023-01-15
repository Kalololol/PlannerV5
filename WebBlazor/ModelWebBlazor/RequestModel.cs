namespace WebBlazor.ModelWebBlazor
{
    public class RequestModel
    {
        public int Id { get; set; }
        public DateTime DayIndisposition { get; set; }
        public char Change { get; set; } //D-Day, N-Night, A-AllDay
        public char TypeRequest { get; set; } // W-Work, H-Holiday
        public int EmployeeId { get; set; }
        public bool Active { get; set; }
    }
}
