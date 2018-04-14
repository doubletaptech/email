using JetBrains.Annotations;

namespace DoubleTap.Email
{
    public class From
    {
        public From(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public From(string emailAddress, string displayName)
        {
            EmailAddress = emailAddress;
            DisplayName  = displayName;
        }

        /// <summary>
        ///     Gets the email address to send the email from
        /// </summary>
        public string EmailAddress { get; }

        /// <summary>
        ///     Gets the name that is displayed in place of the email address when the email is sent.
        /// </summary>
        [CanBeNull]
        public string DisplayName { get; }
    }
}