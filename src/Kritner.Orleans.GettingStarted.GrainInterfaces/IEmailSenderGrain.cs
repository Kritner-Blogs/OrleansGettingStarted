using System.Threading.Tasks;
using Orleans;

namespace Kritner.Orleans.GettingStarted.GrainInterfaces;

/// <summary>
/// A grain abstraction that allows for the sending of an email.
/// </summary>
public interface IEmailSenderGrain : IGrainWithGuidKey, IGrainInterfaceMarker
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="from">The email address sending the email.</param>
    /// <param name="to">The email address(es) to which the email should be delivered.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A <see cref="Task"/> representing the work.</returns>
    Task SendEmail(string from, string[] to, string subject, string body);
}
