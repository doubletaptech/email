using System;

namespace DoubleTap.Email
{
    public interface IEmailFactory
    {
        Email Create(Action<EmailBuilder> email);

        Email CreateFor(string category, Action<EmailBuilder> email);
    }
}