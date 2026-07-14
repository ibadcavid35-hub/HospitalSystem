using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Registration.DoctorPanel
{
    public class Doctor : Person
    {
        public int Experience { get; set; }
        public override string ToString()
        {
            return base.ToString() + $"\n\u001b[32mExperience:\u001b[0m {Experience}";
        }
    }
}
