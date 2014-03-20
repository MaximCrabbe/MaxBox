using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MaxBox.Core.Models
{
    public class GmailSmtpClient : SmtpClient
    {
        public GmailSmtpClient(string emailAddress, string password)
            : base("smtp.gmail.com", 587)
        {
            if (!emailAddress.Contains("@"))
            {
                emailAddress = emailAddress + "@gmail.com";
            }
            EnableSsl = true;
            UseDefaultCredentials = false;
            Credentials = new NetworkCredential(emailAddress, password);
        }
    }
}
