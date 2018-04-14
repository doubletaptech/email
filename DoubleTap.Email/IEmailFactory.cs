using System;

namespace DoubleTap.Email
{
    public interface IEmailFactory
    {
        Email Create(Action<EmailBuilder> email);

        /// <exception cref="ArgumentException">TThrown when the Audience for the specified category can not be found.</exception>
        /// <exception cref="EmailBuilderException">Thrown if 'To' and 'From' have been modified.</exception>
        Email CreateFor(string category, Action<EmailBuilder> email);
    }
}