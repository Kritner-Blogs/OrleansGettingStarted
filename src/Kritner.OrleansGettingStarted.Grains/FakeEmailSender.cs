using Kritner.OrleansGettingStarted.GrainInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.Grains
{
    public class FakeEmailSender : IEmailSender
    {
        private readonly ILogger<FakeEmailSender> _logger;

        public FakeEmailSender(ILogger<FakeEmailSender> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Pretend this actually sends an email.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string from, string[] to, string subject, string body)
        {
            var emailBuilder = new StringBuilder();
            emailBuilder.AppendLine("Sending new Email...");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine($"From: {from}");
            emailBuilder.AppendLine($"To: {string.Join(", ", to)}");
            emailBuilder.AppendLine($"Subject: {subject}");
            emailBuilder.AppendLine($"Body: {Environment.NewLine}{body}");

            _logger.LogInformation(emailBuilder.ToString());

            return Task.CompletedTask;
        }
    }
}
