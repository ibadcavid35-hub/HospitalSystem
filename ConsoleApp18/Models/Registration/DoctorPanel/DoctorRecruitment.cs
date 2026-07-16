using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Registration.DoctorPanel
{
    public class DoctorRecruitment
    {
        private static readonly string pendingPath = "pending_doctors.json";
        private static readonly string doctorPath = "doctors.json";

        public static List<Doctor> ReadDoctors(bool isPendingFile)
        {
            string path = (isPendingFile ? pendingPath : doctorPath);
            if (!File.Exists(path)) { return new List<Doctor>(); }
            string readAllDoctors = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Doctor>>(readAllDoctors) ?? new List<Doctor>();
        }

        public static void SaveDoctors(List<Doctor> doctors,bool isPendingFile)
        {
            string path = (isPendingFile) ? pendingPath : doctorPath;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string save = JsonSerializer.Serialize(doctors, options);
            File.WriteAllText(path, save);
        }
    }
}
