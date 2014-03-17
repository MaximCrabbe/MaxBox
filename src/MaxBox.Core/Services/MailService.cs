﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using MaxBox.Core.Models;

namespace MaxBox.Core.Services
{
    public class MailService : IMailService
    {
        private SmtpClient _smtpClient;
        private bool _isInitalized = false;

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
           _smtpClient.Send(mailModel.From, mailModel.To, mailModel.Subject, mailModel.Body);
        }
    }
}
