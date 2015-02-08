using System;
using System.Net.Mail;
using MaxBox.Core.Models;

namespace MaxBox.Core.Services
{
    public class MailService : IMailService
    {
        private bool _isInitalized;
        private SmtpClient _smtpClient;

        public void Initialize(SmtpClient smtpClient)
        {
            _isInitalized = true;
            _smtpClient = smtpClient;
        }

        public void SendMail(MailModel mailModel)
        {
            if (!_isInitalized)
            {
                throw new Exception("Please use the initialize method first to define a smtp client");
            }
            if (mailModel.AllowHtml)
            {
                mailModel.Body = mailModel.Body.Replace("\n", "<br />");
            }
            var mailMessage = new MailMessage(mailModel.From, mailModel.To, mailModel.Subject, mailModel.Body);
            mailMessage.IsBodyHtml = mailModel.AllowHtml;

            _smtpClient.Send(mailMessage);
        }
    }
}