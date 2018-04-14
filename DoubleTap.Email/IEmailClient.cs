using System.Threading.Tasks;

namespace DoubleTap.Email
{
    public interface IEmailClient
    {
        /// <exception cref="EmailClientException">Thrown when there is a problem when sending the email.</exception>
        Task SendAsync(Email email);
    }
}