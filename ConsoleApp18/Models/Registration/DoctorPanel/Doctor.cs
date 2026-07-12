using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Registration.DoctorPanel
{
    public class Doctor
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Experience { get; set; }
        public override string ToString() =>
            $@"Fullname: {Name} {Surname}
Experience: {Experience}";
    }
}
