using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Mesajlasma
{
    public interface IEmailSenderService
    {
        EmailSendResult Send(EmailMessageModel model);
    }
}
