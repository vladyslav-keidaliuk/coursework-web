﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace CourseWorkWeb.Data
{
    public class EmailSender :IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // logic to send Email 
            return Task.CompletedTask;
        }
    }
}
