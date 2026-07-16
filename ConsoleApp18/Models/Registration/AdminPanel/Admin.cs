using ConsoleApp18.Models.Registration.DoctorPanel;
using ConsoleApp18.Models.Registration.DoctorPanel.ConsoleApp18.Models.Registration.DoctorPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp18.Models.CustomException;
using ConsoleApp18.Models.History;

namespace ConsoleApp18.Models.Registration.AdminPanel
{
    public static class Admin
    {
        public static void ApproveDoctors()
        {
            Logger.SaveToCheck("Approve Doctor");
            Console.Clear();
            Console.WriteLine("=== Admin confirmation panel ===");

            List<Doctor> pendingList = DoctorRecruitment.ReadDoctors(isPendingFile: true);

            if (pendingList.Count == 0)
            {
                Console.WriteLine("There are no pending applications.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Pending applications\n------------------------------------");
            foreach (var doc in pendingList)
            {
                Console.WriteLine($"Həkim: {doc.Name} | Email: {doc.Email} | Kafedra: {doc.Department}");
            }
            Console.WriteLine("------------------------------------");

            Console.Write("\nEnter the email address of the doctor you want to verify: ");
            string selectedEmail = Console.ReadLine();

            Doctor approvedDoc = pendingList.Find(d => d.Email.Equals(selectedEmail, StringComparison.OrdinalIgnoreCase))?? new Doctor();

            if (approvedDoc != null)
            {
                approvedDoc.IsApproved = true;

                List<Doctor> mainList = DoctorRecruitment.ReadDoctors(isPendingFile: false);
                mainList.Add(approvedDoc);
                DoctorRecruitment.SaveDoctors(mainList, isPendingFile: false);

                // Müvəqqəti siyahıdan silirik
                pendingList.Remove(approvedDoc);
                DoctorRecruitment.SaveDoctors(pendingList, isPendingFile: true);

                Console.WriteLine($"\n[Successful] {approvedDoc.Name} {approvedDoc.Surname} approved and added to the main system!");
            }
            else
            {
                try
                {
                    throw new NotFoundException("No doctor's appointment was found that matched the email address entered.");
                }
                catch(NotFoundException nfe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(nfe.Message);Console.ResetColor();
                }
            }
            Console.ReadLine();
        }

    }
}
