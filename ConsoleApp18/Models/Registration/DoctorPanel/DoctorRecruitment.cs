using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using ConsoleApp18.Models.Files;

namespace ConsoleApp18.Models.Registration.DoctorPanel
{

    public static class DoctorRecruitment
    {
        private static readonly string PendingFilePath = "pending_doctors";
        private static readonly string MainFilePath = "doctors";

        public static List<Doctor> ReadDoctors(bool isPendingFile)
        {
            string path = isPendingFile ? PendingFilePath : MainFilePath;
            return FileHelper.LoadData<Doctor>(path);
        }

        public static void SaveDoctors(List<Doctor> doctors, bool isPendingFile)
        {
            string path = isPendingFile ? PendingFilePath : MainFilePath;
            FileHelper.SaveData<Doctor>(doctors, path);
        }

    }
}