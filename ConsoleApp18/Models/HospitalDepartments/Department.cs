using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.HospitalDepartments
{
    public static class Department
    {
        public static void SelectDepartment()
        {
            int select = 0;
            bool isRun = true;
            Thread.Sleep(3000);
            while (isRun)
            {
                Console.Clear();
                string[] choice = { "Pediatrics", "Traumatology", "Dentistry", "Back" };
                for (int i = 0; i < choice.Length; i++)
                {
                    if(i == select)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($">>  {choice[i]}"); Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    {choice[i]}");
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if(key.Key == ConsoleKey.UpArrow && select > 0) { select--; }
                if (key.Key == ConsoleKey.DownArrow && select < choice.Length - 1) { select++; }
                if(key.Key == ConsoleKey.Enter)
                {
                    if (select == 0)
                    {

                        Console.ReadKey(true);
                    }
                    else if (select == 1)
                    {

                        Console.ReadKey(true);
                    }
                    else if (select == 2)
                    {

                        Console.ReadKey(true);
                    }
                    else if (select == 3) { isRun = false; break; }
                }
            }
        }
    }
}
