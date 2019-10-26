using System;
using HangfireExample.Services.Interfaces;

namespace HangfireExample.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public void Send()
        {
            Console.WriteLine("Sending email...");
        }
    }
}
