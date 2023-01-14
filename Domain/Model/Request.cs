using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Request : Entity
    {
        public DateTime DayIndisposition { get; set; }
        public char Change { get; set; } //D-Day, N-Night, A-AllDay
        public char TypeRequest { get; set; } // W-Work, H-Holiday
        public int EmployeeId { get; set; }
        public bool Active { get; set; }
    }
}

