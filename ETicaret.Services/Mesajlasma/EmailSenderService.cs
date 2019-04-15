using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Mesajlasma
{
    public class EmailSenderService : IEmailSenderService
    {
        public EmailSendResult Send(EmailMessageModel model)
        {
            var message = new MailMessage();
            foreach (var item in model.Receiver)
            {
                message.To.Add(new MailAddress(item));
            }
            message.From = new MailAddress("onur@noktanetakademi.com");
            message.Subject = model.Subject;
            message.Body = model.MessageBody;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            var credential = new NetworkCredential
            {
                UserName = "onur@noktanetakademi.com",
                Password = "aJweox9XYpWq"
            };
            smtp.Credentials = credential;
            smtp.Host = "mail.noktanetakademi.com";
            smtp.Port = 587;
            smtp.EnableSsl = false;
            smtp.Send(message);
            return EmailSendResult.Success;
        }
    }
}
