using System.Net.Mail;
using MaxBox.Core.Models;

namespace MaxBox.Core.Services
{
    public interface IMailService
    {
        void Initialize(SmtpClient smtpClient);
        void SendMail(MailModel mailModel);
    }
}