using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.CustomException
{
    public class AlreadyExistExceptipon : ApplicationException
    {
        public AlreadyExistExceptipon() : base("This email and phone number already exist.") { }
    }
}
