using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

// ReSharper disable ArgumentsStyleNamedExpression

namespace DoubleTap.Email
{
    [PublicAPI]
    public class EmailBuilder
    {
        readonly List<Attachment> _attachments = new List<Attachment>();
        readonly IEmailClient _emailClient;
        readonly ITemplateService _templateService;
        bool _asHtml;
        string _body;
        From _from;
        object _model;
        string _subject;
        string _templateKey;
        string[] _to;
        string[] _cc;
        string[] _bcc;

        internal EmailBuilder(IEmailClient emailClient, ITemplateService templateService)
        {
            _emailClient     = emailClient;
            _templateService = templateService;
        }

        public EmailBuilder To(params string[] to)
        {
            _to = to;
            return this;
        }

        public EmailBuilder Cc(params string[] cc)
        {
            _cc = cc;
            return this;
        }

        public EmailBuilder Bcc(params string[] bcc)
        {
            _bcc = bcc;
            return this;
        }

        public EmailBuilder From(string from)
        {
            _from = new From(from);
            return this;
        }

        public EmailBuilder From(string emailAddress, string displayName)
        {
            _from = new From(emailAddress, displayName);
            return this;
        }

        public EmailBuilder SubjectIs(string subject)
        {
            _subject = subject;
            return this;
        }

        /// <summary>
        ///     Specify the model that will be used for the email body.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model</param>
        /// <param name="templateKey">
        ///     The template key to resolve the template that will transform the model into a string.
        ///     If this is not provided then the type of the model is used as the key.
        /// </param>
        /// <returns>The current <see cref="EmailBuilder" /></returns>
        /// <exception cref="EmailBuilderException">Thrown if a body has been supplied previously.</exception>
        public EmailBuilder WithModel<TModel>(TModel model, string templateKey = null)
        {
            if (_body != null)
                throw new EmailBuilderException("You cannot supply a model if a body has been supplied previously.");

            _model       = model;
            _templateKey = templateKey;
            return this;
        }

        /// <summary>
        ///     Specify the body of the email.
        /// </summary>
        /// <param name="body">The email body</param>
        /// <returns>The current <see cref="EmailBuilder" /></returns>
        /// <exception cref="EmailBuilderException">Thrown if a model has been supplied previously.</exception>
        public EmailBuilder WithBody(string body)
        {
            if (_model != null)
                throw new EmailBuilderException("You cannot supply a body if a model been supplied previously.");

            _body = body;
            return this;
        }

        /// <summary>
        ///     Indicate that the body of the email should render as HTML
        /// </summary>
        /// <returns>The current <see cref="EmailBuilder" /></returns>
        public EmailBuilder AsHtml()
        {
            _asHtml = true;
            return this;
        }

        public EmailBuilder WithAttachments(params string[] filepaths)
        {
            _attachments.AddRange(filepaths.Select(x => new Attachment(x, x)));
            return this;
        }

        public EmailBuilder WithAttachments(params Attachment[] attachments)
        {
            _attachments.AddRange(attachments);
            return this;
        }

        [SuppressMessage("ReSharper", "ArgumentsStyleAnonymousFunction")]
        internal Email Build()
        {
            ensureSuppliedValuesAreValid();

            var body = _model != null ? applyTemplate(_model) : _body;

            return new Email(
                from: _from,
                to: _to,
                cc: _cc,
                bcc: _bcc,
                subject: _subject,
                body: body,
                attachments: _attachments,
                isBodyHtml: _asHtml,
                sendAsync: email => _emailClient.SendAsync(email));
        }

        string applyTemplate(object model)
        {
            var modelType = model.GetType();
            var template = $"{modelType.Name}";

            return _templateService.Apply(template, _model);
        }

        void ensureSuppliedValuesAreValid()
        {
            if (_to == null)
                throw new EmailBuilderException(
                    $"You must specify an address to send the email to by calling '{nameof(To)}()'.");

            if (_subject == null)
                throw new EmailBuilderException($"You must specify the subject calling '{nameof(SubjectIs)}()'.");

            if (_body == null && _model == null)
                throw new EmailBuilderException(
                    $"You must specify a body or a model by calling '{nameof(WithBody)}()' or '{nameof(WithModel)}()'.");
        }
    }
}