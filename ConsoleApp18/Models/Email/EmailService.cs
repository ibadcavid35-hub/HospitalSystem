using ConsoleApp18.Models.History;
using ConsoleApp18.Models.Registration.DoctorPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18.Models.Email
{
    public static class EmailService
    {
        public static void SendCVtoAdmin(Doctor doctor)
        {
            Logger.SaveToCheck("Email sent.");
            string adminEmail = "ibadcavid35@gmail.com";
            string appPassword = "hiab nulk tuks tytx";

            try
            {
   
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(adminEmail);
                mail.To.Add(adminEmail); 
                mail.Subject = $"[New doctor's appointment] - {doctor.Name}";
                mail.Body = $"Admin, there is a new request:\nName: {doctor.Name} {doctor.Surname}\nEmail: {doctor.Email}";

                if (!string.IsNullOrEmpty(doctor.CVPath) && System.IO.File.Exists(doctor.CVPath))
                {
                    mail.Attachments.Add(new Attachment(doctor.CVPath));
                }

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(adminEmail, appPassword);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                Console.WriteLine("SMTP: The message was sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was a SMTP error: " + ex.Message);
            }
        }
    }
}
