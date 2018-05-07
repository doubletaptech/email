using JetBrains.Annotations;

namespace DoubleTap.Email
{
    /// <summary>
    ///     Defines an audience for a category of emails
    /// </summary>
    [PublicAPI]
    public interface IEmailAudience
    {
        /// <summary>
        ///     The addressee of the email
        /// </summary>
        [NotNull]
        string[] To { get; }

        /// <summary>
        ///     The email sender
        /// </summary>
        [CanBeNull]
        From From { get; }

        /// <summary>
        ///     Email addresses to Cc the email to
        /// </summary>
        [CanBeNull]
        string[] Cc { get; }

        /// <summary>
        ///     Email addresses to Bcc the email to
        /// </summary>
        [CanBeNull]
        string[] Bcc { get; }

        /// <summary>
        ///     Checks whether this <see cref="IEmailAudience" /> targets a particular category
        /// </summary>
        /// <param name="category">Category to check</param>
        /// <returns></returns>
        bool Targets(string category);
    }
}