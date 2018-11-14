using System.Threading.Tasks;

namespace Kritner.OrleansGettingStarted.GrainInterfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string[] to, string subject, string body);
    }
}
