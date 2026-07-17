using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.CommonNumber
{
    public static class CommonNumberClass
    {
        public static string CommonPhoneNumber(string number)
        {
            number = number.Replace(" ", "");
            if (number.StartsWith("+994") && number.Length == 13) { return number.Substring(4); }
            if (number.StartsWith("0") && number.Length == 10) { return number.Substring(1); }
            return number;
        }
    }
}
