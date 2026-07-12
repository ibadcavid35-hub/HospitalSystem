using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Exception
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base($"User not found with this {message}! Please register.\"") { }
    }
}
