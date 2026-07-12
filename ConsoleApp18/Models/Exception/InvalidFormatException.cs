using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Exception
{
    public class InvalidFormatException : ApplicationException
    {
        public InvalidFormatException(string message) : base($"Invalid {message} format! Try again.") { }
    }
}
