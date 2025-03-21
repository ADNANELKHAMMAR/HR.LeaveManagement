﻿using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get;  }
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(EmailMessage email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress { Email = email.To };
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAdress,
                Name = _emailSettings.FromName
            };
            var message = MailHelper.CreateSingleEmail(from, to,email.Subject,email.Body,email.Body);
            var Response = await client.SendEmailAsync(message);
            return Response.StatusCode == System.Net.HttpStatusCode.OK
                    || Response.StatusCode == System.Net.HttpStatusCode.Accepted
                    || Response.IsSuccessStatusCode;
        }
    }
}
