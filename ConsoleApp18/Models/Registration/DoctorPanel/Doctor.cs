using ConsoleApp18.Models.Appointments;
using ConsoleApp18.Models.HospitalDepartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp18.Models.Exception;

namespace ConsoleApp18.Models.Registration.DoctorPanel
{
    public class Doctor : Person
    {
        public int Experience { get; set; }
        public DepartmentType Department { get; set; }
        public List<TimeSlot> TimeSlot { get; set; } = new List<TimeSlot>
        {
            new TimeSlot { Time = "09:00-11:00" },
            new TimeSlot { Time = "12:00-14:00" },
            new TimeSlot { Time = "15:00-17:00" }
        };

        public void ToEnterExtra()
        {
            Console.Write("Enter your experience (years): ");
            int experience;
            while (!int.TryParse(Console.ReadLine(), out experience))
            {
                try
                {
                    throw new InvalidFormatException("Invalid input! Please enter a positive number.");
                }
                catch(InvalidFormatException ife)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ife.Message);
                    Console.ResetColor();
                }
                Console.Write("Enter your experience (years): ");
            }
            Experience = experience;

            DepartmentType[] departments = Enum.GetValues<DepartmentType>();
            int select = 0;
            bool isRun = true;
            while (isRun)
            {
                Console.Clear();
                Console.WriteLine("Select your department:");
                for (int i = 0; i < departments.Length; i++)
                {
                    if (i == select)
                    {
                        Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">>  {departments[i]}"); Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {departments[i]}");
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow && select > 0) { select--; }
                if (key.Key == ConsoleKey.DownArrow && select < departments.Length - 1) { select++; }
                if (key.Key == ConsoleKey.Enter)
                {
                    Department = departments[select];
                    isRun = false;
                }
            }
        }
        public override string ToString()
        {
            return base.ToString() + $"\n\u001b[32mExperience:\u001b[0m {Experience}";
        }
    }
}
