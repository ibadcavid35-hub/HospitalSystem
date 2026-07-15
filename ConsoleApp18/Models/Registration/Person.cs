using ConsoleApp18.Models.Exception;
using ConsoleApp18.Models.Files;
using ConsoleApp18.Models.HospitalDepartments;
using ConsoleApp18.Models.History;
using ConsoleApp18.Models.Registration.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Registration
{
    public class Person
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Number { get; set; }

        public override string ToString()
        {
            return $"\u001b[32mName:\u001b[0m {Name}\n" +
                   $"\u001b[32mSurname:\u001b[0m {Surname}\n" +
                   $"\u001b[32mEmail:\u001b[0m {Email}\n" +
                   $"\u001b[32mNumber:\u001b[0m {Number}";
        }

        public void ToEnter()
        {
            Logger.SaveToCheck("Entered");
            Console.Write("Enter your name: "); Name = Console.ReadLine();
            Console.Write("Enter your surname: "); Surname = Console.ReadLine();
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            while (true)
            {
                try
                {
                    Console.Write("Enter your email: "); Email = Console.ReadLine() ?? "";
                    if (Regex.IsMatch(Email, emailPattern)) break;
                    throw new InvalidFormatException("email");
                }
                catch (InvalidFormatException ife)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ife.Message); Console.ResetColor();
                }
            }
            string phonePattern = @"^(?:\+994|994|0)?(?:50|51|55|70|77|99|10|60)\d{7}$";
            while (true)
            {
                try
                {
                    Console.Write("Enter your phone number: "); string inputNumber = Console.ReadLine() ?? "";
                    if (Regex.IsMatch(inputNumber, phonePattern))
                    {
                        Number = CommonNumber.CommonNumber.CommonPhoneNumber(inputNumber);
                        break;
                    }
                    else throw new InvalidFormatException("phone number");
                }
                catch (InvalidFormatException ife)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ife.Message); Console.ResetColor();
                }
            }
            ToEnterExtra();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have registered."); Console.ResetColor();
        }

        public virtual void ToEnterExtra() { }

        public void Registr<T>(List<T> People) where T : Person, new()
        {
            Logger.SaveToCheck("Registered");
            int select = 0;
            bool isRun = true;
            while (isRun)
            {
                Console.Clear();
                string[] choices = { "Registration", "Login", "Back" };
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
                        Console.Clear();
                        T person = new T();
                        person.ToEnter();
                        bool isExist = People!.Any(x => x.Email == person.Email || x.Number == person.Number);
                        try
                        {
                            if (!isExist)
                            {
                                People!.Add(person);
                                string fileName = typeof(T) == typeof(User) ? "users" : "doctors";
                                FileHelper.SaveData(People, fileName);
                                if (typeof(T) == typeof(User))
                                {
                                    bool registered = Department.SelectDepartment(person);
                                    if (registered) { isRun = false; }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Your registration has been recorded. Your application for admission has been sent to the Admin.");
                                    Console.ResetColor();
                                }
                            }
                            else throw new AlreadyExistExceptipon();
                        }
                        catch (AlreadyExistExceptipon aee)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(aee.Message); Console.ResetColor();
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey(true);
                    }
                    else if (select == 1)
                    {
                        Console.Clear();
                        int choose = 0;
                        bool isLogged = true;
                        while (isLogged)
                        {
                            Console.Clear();
                            string[] logChoices = { "Email", "Phone number", "Back" };
                            for (int i = 0; i < logChoices.Length; i++)
                            {
                                if (i == choose)
                                {
                                    Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine($">>  {logChoices[i]}"); Console.ResetColor();
                                }
                                else
                                {
                                    Console.WriteLine($"    {logChoices[i]}");
                                }
                            }
                            ConsoleKeyInfo key2 = Console.ReadKey(true);
                            if (key2.Key == ConsoleKey.UpArrow && choose > 0) { choose--; }
                            if (key2.Key == ConsoleKey.DownArrow && choose < choices.Length - 1) { choose++; }
                            if (key2.Key == ConsoleKey.Enter)
                            {
                                if (choose == 0)
                                {
                                    Console.Clear();
                                    Console.Write("Enter your email: ");
                                    string inputEmail = Console.ReadLine() ?? "";
                                    Person? foundUser = People!.FirstOrDefault(x => x.Email == inputEmail);
                                    try
                                    {
                                        if (foundUser != null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\n--- Login Successful! Your Profile ---");
                                            Console.ResetColor();
                                            Console.WriteLine(foundUser);
                                            if (typeof(T) == typeof(User))
                                            {
                                                bool registered = Department.SelectDepartment(foundUser);
                                                if (registered) { isLogged = false; isRun = false; }
                                            }
                                            else
                                            {

                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("You can check your job application status in the Admin Panel.");
                                                Console.ResetColor();
                                                Console.WriteLine("Press any key to continue...");
                                                Console.ReadKey(true);

                                            }
                                        }
                                        else
                                        {
                                            throw new NotFoundException($"User not found with this {inputEmail}! Please register.\"");
                                        }
                                    }
                                    catch (NotFoundException nfe)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(nfe.Message); Console.ResetColor();
                                    }

                                }
                                else if (choose == 1)
                                {
                                    Console.Clear();
                                    Console.Write("Enter your phone number: ");
                                    string inputNumber = Console.ReadLine() ?? "";
                                    inputNumber = CommonNumber.CommonNumber.CommonPhoneNumber(inputNumber);
                                    Person? foundUser = People!.FirstOrDefault(x => x.Number == inputNumber);
                                    try
                                    {
                                        if (foundUser != null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\n--- Login Successful! Your Profile ---");
                                            Console.ResetColor();
                                            Console.WriteLine(foundUser);
                                            if (typeof(T) == typeof(User))
                                            {
                                                bool registered = Department.SelectDepartment(foundUser);
                                                if (registered) { isLogged = false; isRun = false; }
                                            }
                                            else
                                            {

                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("You can check your job application status in the Admin Panel.");
                                                Console.ResetColor();
                                                Console.WriteLine("Press any key to continue...");
                                                Console.ReadKey(true);

                                            }
                                        }
                                        else
                                        {
                                            throw new NotFoundException($"User not found with this {inputNumber}! Please register.\"");
                                        }
                                    }
                                    catch (NotFoundException nfe)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(nfe.Message); Console.ResetColor();
                                    }

                                }
                                else if (choose == 2) { isLogged = false; break; }
                            }
                        }
                    }
                    else if (select == 2) { isRun = false; break; }
                }
            }
        }
    }

}
