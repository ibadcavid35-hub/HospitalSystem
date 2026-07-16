using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.CustomException
{
    public class ReservedException : ApplicationException
    {
        public ReservedException():base("This time is already reserved, please choose another time.") { }
    }
}
