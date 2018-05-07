using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace DoubleTap.Email
{
    public class Email
    {
        [NotNull] readonly Func<Email, Task> _sendAsync;

        internal Email([CanBeNull] From from,
                       [NotNull] string[] to,
                       [CanBeNull] string[] cc,
                       [CanBeNull] string[] bcc,
                       [CanBeNull] string subject,
                       [NotNull] string body,
                       [CanBeNull] IReadOnlyList<Attachment> attachments,
                       bool isBodyHtml,
                       [NotNull] Func<Email, Task> sendAsync)
        {
            _sendAsync  = sendAsync;
            From        = from;
            To          = to;
            Cc          = cc ?? new string[0];
            Bcc         = bcc ?? new string[0];
            Subject     = subject;
            Body        = body;
            Attachments = attachments ?? new Attachment[0];
            IsBodyHtml  = isBodyHtml;
        }

        [CanBeNull]
        public From From { get; }

        [NotNull]
        public string[] To { get; }

        [NotNull]
        public string[] Cc { get; }

        [NotNull]
        public string[] Bcc { get; }

        [CanBeNull]
        public string Subject { get; }

        [NotNull]
        public string Body { get; }

        [NotNull]
        public IReadOnlyList<Attachment> Attachments { get; }

        public bool IsBodyHtml { get; }

        public Task SendAsync()
        {
            return _sendAsync(this);
        }
    }
}