using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Appointments
{
    public class TimeSlot
    {
        public string? Time { get; set; }
        public bool IsReserved { get; set; }
        public string? ReservedByName { get; set; }
    }
}
