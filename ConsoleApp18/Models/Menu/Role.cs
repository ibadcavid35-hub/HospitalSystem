using ConsoleApp18.Models.History;
using ConsoleApp18.Models.Registration;
using ConsoleApp18.Models.Registration.AdminPanel;
using ConsoleApp18.Models.Registration.DoctorPanel;
using ConsoleApp18.Models.Registration.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Menu
{
    public static class Role
    {
        public static void ChooseRole()
        {
            Logger.SaveToCheck("Role was chosen");
            int select = 0;
            bool isRun = true;
            while (isRun)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== WELCOME TO HOSPITAL ===");
                Console.ResetColor();
                string[] choices = { "Admin ", "User", "Doctor", "Exit" };
                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == select)
                    {
                        Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">>  {choices[i]}"); Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {choices[i]}");
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow && select > 0) { select--; }
                if (key.Key == ConsoleKey.DownArrow && select < choices.Length - 1) { select++; }
                if (key.Key == ConsoleKey.Enter)
                {
                    if (select == 0)
                    {
                        Admin.ApproveDoctors();
                    }
                    else if (select == 1)
                    {
                        User user = new User();
                        user.Registr(UserRegistration.Users);
                    }
                    else if (select == 2)
                    {
                        Doctor doctor = new Doctor();
                        doctor.Registr(DoctorRegistration.Doctors);
                    }
                    else if (select == 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("You exited"); Console.ResetColor(); break;
                    }
                }
            }
        }
    }
}
