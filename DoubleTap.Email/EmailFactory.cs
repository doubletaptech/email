using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace DoubleTap.Email
{
    public class EmailFactory : IEmailFactory
    {
        readonly IEmailAudience[] _audiences;
        readonly IEmailClient _emailClient;
        readonly ITemplateService _templateService;

        public EmailFactory(IEmailClient emailClient,
                            ITemplateService templateService,
                            params IEmailAudience[] audiences)
        {
            _emailClient     = emailClient;
            _templateService = templateService;
            _audiences       = audiences ?? new IEmailAudience[0];
        }

        public Email Create(Action<EmailBuilder> email)
        {
            var builder = new EmailBuilder(_emailClient, _templateService);
            email(builder);
            return builder.Build();
        }

        /// <exception cref="T:System.ArgumentException">TThrown when the Audience for the specified category can not be found.</exception>
        /// <exception cref="T:DoubleTap.Email.EmailBuilderException">Thrown if 'To' and 'From' have been modified.</exception>
        public Email CreateFor(string category, Action<EmailBuilder> email)
        {
            var audience = getAudience(category);

            return buildEmail(audience, email);
        }

        public Email CreateFor(IEmailAudience audience, Action<EmailBuilder> email)
        {
            return buildEmail(audience, email);
        }

        Email buildEmail(IEmailAudience audience, Action<EmailBuilder> email)
        {
            var builder = new EmailBuilder(_emailClient, _templateService);

            builder.To(audience.To);

            if (audience.From != null)
            {
                builder.From(audience.From.EmailAddress);
            }

            email(builder);

            var builtEmail = builder.Build();

            ensureToAndFromHaveNotBeenModifed(builtEmail, audience);
            ensureCcAndBccHaveNotBeenModifed(builtEmail, audience);

            return builtEmail;
        }

        IEmailAudience getAudience(string category)
        {
            var audience = _audiences.FirstOrDefault(emailAudience => emailAudience.Targets(category));

            if (audience == null)
                throw new ArgumentException(
                    $"An audience for category '{category}' could not be found. " +
                    $"Ensure you have supplied an appropriate implementation of {nameof(IEmailAudience)}.");

            return audience;
        }

        [AssertionMethod]
        void ensureToAndFromHaveNotBeenModifed(Email builtEmail, IEmailAudience audience)
        {
            if (builtEmail.To != audience.To || builtEmail.From != audience.From)
                throw new EmailBuilderException(
                    $"You can not modify the '{nameof(Email.To)}' or '{nameof(Email.From)}' parameters when targeting a specific audience. " +
                    $"Use the '{nameof(Create)}' method if you need control over these.");
        }

        [AssertionMethod]
        void ensureCcAndBccHaveNotBeenModifed(Email builtEmail, IEmailAudience audience)
        {
            if (builtEmail.Cc != audience.Cc || builtEmail.Bcc != audience.Bcc)
                throw new EmailBuilderException(
                    $"You can not modify the '{nameof(Email.Cc)}' or '{nameof(Email.Bcc)}' parameters when targeting a specific audience. " +
                    $"Use the '{nameof(Create)}' method if you need control over these.");
        }
    }
}