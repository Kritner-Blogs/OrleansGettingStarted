using System.Threading.Tasks;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// An abstraction for sending an email
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="from">The email address sending the email.</param>
    /// <param name="to">The email address(es) to which the email should be delivered.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A <see cref="Task"/> representing the work.</returns>
    Task SendEmailAsync(string from, string[] to, string subject, string body);
}
