using System;
using JetBrains.Annotations;

namespace DoubleTap.Email
{
    [PublicAPI]
    public interface IEmailFactory
    {
        Email Create(Action<EmailBuilder> email);

        Email CreateFor(string category, Action<EmailBuilder> email);

        Email CreateFor(IEmailAudience audience, Action<EmailBuilder> email);
    }
}