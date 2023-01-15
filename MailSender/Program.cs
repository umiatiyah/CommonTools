using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SimpleMailSender
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("---Start---");

            string fullName = "manusia";
            string email = "examplemailumi@gmail.com";

            try {
                // read template email and parameter
                string html = string.Format(File.ReadAllText("templates/send_email.html"),
                    fullName,
                    email
                    );

                // read configuration in appsettings.json
                var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", true, true);
                var config = builder.Build();
                
                string? hostEmail = config["Config:Email:Host"];
                int portEmail = Convert.ToInt32(config["Config:Email:Port"]);
                string? usernameEmail = config["Config:Email:Username"];
                string? passwordEmail = config["Config:Email:Password"];
                string? fromEmail = config["Config:Email:From"];
                string? fromName = config["Config:Email:FromName"];

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(fromName, fromEmail));
                mimeMessage.To.Add(new MailboxAddress(fullName, email));
                mimeMessage.Subject = "MeetMe";
                mimeMessage.Body = new TextPart("html")
                {
                    Text = html
                };

                // set SmtpClient
                using var client = new SmtpClient();
                await client.ConnectAsync(hostEmail, portEmail, false);
                await client.AuthenticateAsync(usernameEmail, passwordEmail);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.ToString()}");
            }

            Console.WriteLine("Sent Success!");
            Console.ReadKey();
        }
    }
}
