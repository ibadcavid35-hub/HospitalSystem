using ConsoleApp18.Models.CustomException;
using ConsoleApp18.Models.Files;
using ConsoleApp18.Models.Registration;
using ConsoleApp18.Models.Registration.DoctorPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ConsoleApp18.Models.History;

namespace ConsoleApp18.Models.HospitalDepartments
{
    public static class Department
    {
        public static bool SelectDepartment(Person user)
        {
            Logger.SaveToCheck("Department selected");
            int select = 0;
            bool isRun = true;
            Thread.Sleep(2000);
            DepartmentType[] departments = Enum.GetValues<DepartmentType>();
            while (isRun)
            {
                Console.Clear();
                Console.WriteLine("Please select a department:\n");
                string[] choices = departments.Select(d => d.ToString()).Append("Back").ToArray();
                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == select)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">>  {choices[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {choices[i]}");
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && select > 0) { select--; }
                if (key.Key == ConsoleKey.DownArrow && select < choices.Length - 1) { select++; }
                if (key.Key == ConsoleKey.Enter)
                {
                    if (select == choices.Length - 1) { isRun = false; break; }
                    bool reserved = SelectDoctor(departments[select], user);
                    if (reserved) { return true; }
                }
            }
            return false;
        }

        private static bool SelectDoctor(DepartmentType department, Person user)
        {
            Logger.SaveToCheck("Doctor selected");
            var doctorInDept = DoctorRegistration.Doctors.Where(d => d.Department == department && d.IsApproved == true).ToList();
            if (!doctorInDept.Any())
            {
                try
                {
                    throw new NotFoundException("There is no doctor in this department right now.");
                }
                catch (NotFoundException nfe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(nfe.Message);
                    Console.ResetColor();
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                return false;
            }
            int select = 0;
            bool isRun = true;
            while (isRun)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Doctors working in the {department} department");
                Console.ResetColor();
                string[] choices = doctorInDept
                    .Select(d => $"Dr .{d.Name} {d.Surname} | {d.Experience} years of experience")
                    .Append("Back").ToArray();
                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == select)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">>  {choices[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {choices[i]}");
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && select > 0) { select--; }
                if (key.Key == ConsoleKey.DownArrow && select < choices.Length - 1) { select++; }
                if (key.Key == ConsoleKey.Enter)
                {
                    if (select == choices.Length - 1) { isRun = false; break; }
                    bool reserved = SelectSlot(doctorInDept[select], user);
                    if (reserved) { return true; }
                }
            }
            return false;
        }

        private static bool SelectSlot(Doctor doctor, Person user)
        {
            Logger.SaveToCheck("Time slot selected");
            int select = 0;
            bool isRun = true;
            while (isRun)
            {
                Console.Clear();
                Console.WriteLine($"Dr. {doctor.Name} {doctor.Surname} - reception hours:\n");
                string[] choices = doctor.TimeSlot
                    .Select(r => $"{r.Time} -> " + (r.IsReserved ? "\u001b[31mreserved\u001b[0m" : "\u001b[32mnot reserved\u001b[0m"))
                    .Append("Back").ToArray();
                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == select)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">>  {choices[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {choices[i]}");
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && select > 0) { select--; }
                if (key.Key == ConsoleKey.DownArrow && select < choices.Length - 1) { select++; }
                if (key.Key == ConsoleKey.Enter)
                {
                    if (select == choices.Length - 1) { isRun = false; break; }
                    var slot = doctor.TimeSlot[select];
                    if (slot.IsReserved)
                    {
                        Console.Clear();
                        try
                        {
                            throw new ReservedException();
                        }
                        catch (ReservedException re)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(re.Message);
                            Console.ResetColor();
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey(true);
                        continue;
                    }
                    slot.IsReserved = true;
                    slot.ReservedByName = $"{user.Name} {user.Surname}";
                    slot.ReservedByPhone = user.Number;
                    FileHelper.SaveData(DoctorRegistration.Doctors, "doctors");

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{user.Name} {user.Surname}, You have an appointment with Dr. {doctor.Name} {doctor.Surname} at {slot.Time}.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    return true;
                }
            }
            return false;
        }
    }
}
