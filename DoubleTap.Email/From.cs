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

        protected bool Equals(From other)
        {
            return string.Equals(EmailAddress, other.EmailAddress) && string.Equals(DisplayName, other.DisplayName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((From) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((EmailAddress != null ? EmailAddress.GetHashCode() : 0) * 397) ^
                       (DisplayName != null ? DisplayName.GetHashCode() : 0);
            }
        }
    }
}