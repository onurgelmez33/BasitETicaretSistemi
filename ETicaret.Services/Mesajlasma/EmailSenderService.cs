using ETicaret.Services.System;
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
        private ISettingService _settingService;
        public EmailSenderService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public EmailSendResult Send(EmailMessageModel model)
        {
            var email = _settingService.GetSetting<string>("SmtpEmailAddress");
            var senderName = _settingService.GetSetting<string>("EmailSenderName");
            var password = _settingService.GetSetting<string>("SmtpEmailPassword");
            var host = _settingService.GetSetting<string>("SmtpEmailHost");
            var port = _settingService.GetSetting<int>("SmtpEmailPort");
            var isSSL = _settingService.GetSetting<bool>("SmtpEmailSSL");
            var message = new MailMessage();
            foreach (var item in model.Receiver)
            {
                message.To.Add(new MailAddress(item));
            }
            message.From = new MailAddress(email, senderName);
            message.Subject = model.Subject;
            message.Body = model.MessageBody;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            var credential = new NetworkCredential
            {
                UserName = email,
                Password = password
            };
            smtp.Credentials = credential;
            smtp.Host = host;
            smtp.Port = port;
            smtp.EnableSsl = isSSL;
            smtp.Send(message);
            return EmailSendResult.Success;
        }
    }
}
