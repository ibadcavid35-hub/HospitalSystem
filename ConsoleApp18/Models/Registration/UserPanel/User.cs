using ConsoleApp18.Models.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ConsoleApp18.Models.Registration.UserPanel
{
    public class User
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Number { get; set; }

        public override string ToString() =>
    $"\u001b[32mFullname:\u001b[0m {Name} {Surname}" +
            $" \n\u001b[32mEmail:\u001b[0m {Email} \n\u001b[32mNumber:\u001b[0m {Number}";

 
        public void ToEnter()
        {
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
                catch(InvalidFormatException ife) 
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
                catch(InvalidFormatException ife) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ife.Message); Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have registered."); Console.ResetColor();
        }
    }
}