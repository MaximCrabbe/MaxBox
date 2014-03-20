﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MaxBox.Core.Models
{
    public class HotmailSmtpClient : SmtpClient
    {
        public HotmailSmtpClient(string emailAddress, string password)
            : base("smtp.live.com",587)
        {
            if (!emailAddress.Contains("@"))
            {
                emailAddress = emailAddress + "@hotmail.com";
            }
            EnableSsl = true;
            Credentials = new NetworkCredential(emailAddress, password);
        }
    }
}
