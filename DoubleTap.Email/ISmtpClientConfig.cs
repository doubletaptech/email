using JetBrains.Annotations;

namespace DoubleTap.Email
{
    public interface ISmtpClientConfig
    {
        [NotNull] string Host { get; }
        int Port { get; }
        SmtpClientMode Mode { get; }
        [CanBeNull] string DirectoryToWriteEmailsTo { get; }
    }
}