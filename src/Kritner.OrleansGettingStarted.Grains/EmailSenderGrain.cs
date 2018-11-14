using Kritner.OrleansGettingStarted.GrainInterfaces;
using Orleans;
using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.Grains
{
    public class EmailSenderGrain : Grain, IEmailSenderGrain, IGrainMarker
    {
        private readonly IEmailSender _emailSender;

        public EmailSenderGrain(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmail(string from, string[] to, string subject, string body)
        {
            await _emailSender.SendEmailAsync(from, to, subject, body);
        }
    }
}
