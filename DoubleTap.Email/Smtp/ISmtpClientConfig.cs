using System.Net;
using System.Net.Mail;
using JetBrains.Annotations;

namespace DoubleTap.Email.Smtp
{
    public interface ISmtpClientConfig
    {
        [NotNull]
        string Host { get; }

        int Port { get; }

        SmtpClientMode Mode { get; }

        /// <summary>
        ///     Gets the credentials used to authenticate the sender
        /// </summary>
        [CanBeNull]
        NetworkCredential Credentials { get; }

        /// <summary>
        ///     Specify whether the <see cref="SmtpClient" /> uses Secure Sockets Layer (SSL) to encrypt the conneciton.
        ///     Defaults to true if <see cref="Credentials" /> is not specified.
        /// </summary>
        [CanBeNull]
        bool? EnableSsl { get; }

        /// <summary>
        ///     Directory to which emails will be written when <see cref="Mode" /> is set to
        ///     <see cref="SmtpClientMode.Directory" />
        /// </summary>
        [CanBeNull]
        string DirectoryToWriteEmailsTo { get; }
    }
}