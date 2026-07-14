using ConsoleApp18.Models.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Registration.UserPanel
{
    public class UserRegistration : Person
    {
        public static List<User> Users { get; set; } = new List<User>();
     
    }
}
