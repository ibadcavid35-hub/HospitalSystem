using ConsoleApp18.Models.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Registration.UserPanel
{
    public class UserRegistration
    {
        public static List<User>? Users { get; set; } = new List<User>();
        public void UserRegistr()
        {
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
                        User user = new User();
                        user.ToEnter();
                        bool isExist = Users!.Any(x => x.Email == user.Email || x.Number == user.Number);
                        try
                        {
                            if (!isExist) { Users!.Add(user); }
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
                            if (key2.Key == ConsoleKey.UpArrow && select > 0) { choose--; }
                            if (key2.Key == ConsoleKey.DownArrow && select < choices.Length - 1) { choose++; }
                            if (key2.Key == ConsoleKey.Enter)
                            {
                                if (choose == 0)
                                {
                                    Console.Clear();
                                    Console.Write("Enter your email: ");
                                    string inputEmail = Console.ReadLine() ?? "";
                                    User? foundUser = Users!.FirstOrDefault(x => x.Email == inputEmail);
                                    try
                                    {
                                        if (foundUser != null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\n--- Login Successful! Your Profile ---");
                                            Console.ResetColor();
                                            Console.WriteLine(foundUser);
                                        }
                                        else
                                        {
                                            throw new NotFoundException(inputEmail);
                                        }
                                    }
                                    catch (NotFoundException nfe)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(nfe.Message); Console.ResetColor();
                                    }
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey(true);
                                }
                                else if (choose == 1)
                                {
                                    Console.Clear();
                                    Console.Write("Enter your phone number: ");
                                    string inputNumber = Console.ReadLine() ?? "";
                                    inputNumber = CommonNumber.CommonNumber.CommonPhoneNumber(inputNumber);
                                    User? foundUser = Users!.FirstOrDefault(x => x.Number == inputNumber);
                                    try
                                    {
                                        if (foundUser != null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\n--- Login Successful! Your Profile ---");
                                            Console.ResetColor();
                                            Console.WriteLine(foundUser);
                                        }
                                        else
                                        {
                                            throw new NotFoundException(inputNumber);
                                        }
                                    }
                                    catch (NotFoundException nfe)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(nfe.Message); Console.ResetColor();
                                    }
                                    Console.WriteLine("Press any key to continue");
                                    Console.ReadKey(true);
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
