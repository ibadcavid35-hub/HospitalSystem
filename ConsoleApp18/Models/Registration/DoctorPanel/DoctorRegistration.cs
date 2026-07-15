using ConsoleApp18.Models.Exception;
using ConsoleApp18.Models.Files;
using ConsoleApp18.Models.Registration.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Registration.DoctorPanel
{
    public class DoctorRegistration:Person
    {
        public static List<Doctor> Doctors { get; set; } = FileHelper.LoadData<Doctor>("doctors");
        
   
    }
}
