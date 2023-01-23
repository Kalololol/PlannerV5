namespace Application.ModelDto
{
    public class RequestDto
    {
        public int Id { get; set; }
        public DateTime DayIndisposition { get; set; }
        public string Change { get; set; } //D-Day, N-Night, A-AllDay
        public string TypeRequest { get; set; } // W-Work, H-Holiday
        public int EmployeeId { get; set; }
        public bool Active { get; set; }
    }
}
