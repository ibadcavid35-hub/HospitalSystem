using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace ConsoleApp18.Models.Registration.DoctorPanel
{



    public static class DoctorRecruitment
    {
        private static readonly string PendingFilePath = "pending_doctors.json";
        private static readonly string MainFilePath = "doctors.json";

        public static List<Doctor> ReadDoctors(bool isPendingFile)
        {
            string path = isPendingFile ? PendingFilePath : MainFilePath;

            if (!File.Exists(path)) return new List<Doctor>();

            try
            {
                string jsonString = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Doctor>>(jsonString) ?? new List<Doctor>();
            }
            catch
            {
                return new List<Doctor>();
            }
        }

        public static void SaveDoctors(List<Doctor> doctors, bool isPendingFile)
        {
            string path = isPendingFile ? PendingFilePath : MainFilePath;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(doctors, options);
            File.WriteAllText(path, jsonString);
        }

    }
}