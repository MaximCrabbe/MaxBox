using System.Net.Mail;

namespace MaxBox.Core.Models
{
    public class MailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool AllowHtml { get; set; }

        public MailMessage AsMailMessage
        {
            get
            {
                var mailmessage = new MailMessage(From, To, Subject, Body);
                mailmessage.IsBodyHtml = AllowHtml;
                return mailmessage;
            }
        }
    }
}