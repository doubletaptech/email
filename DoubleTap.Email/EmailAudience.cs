using System;
using System.Linq;

namespace DoubleTap.Email
{
    /// <inheritdoc />
    public class EmailAudience : IEmailAudience
    {
        readonly string[] _categories;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailAudience" /> class.
        /// </summary>
        public EmailAudience(string[] categories, string[] to, From from, string[] cc = null, string[] bcc = null)
        {
            _categories = categories;
            To          = to;
            From        = from;
            Cc          = cc;
            Bcc         = bcc;
        }

        /// <inheritdoc />
        public string[] To { get; }

        /// <inheritdoc />
        public From From { get; }

        /// <inheritdoc />
        public string[] Cc { get; }

        /// <inheritdoc />
        public string[] Bcc { get; }

        /// <inheritdoc />
        public bool Targets(string category)
        {
            return _categories.Contains(category, StringComparer.OrdinalIgnoreCase);
        }
    }
}