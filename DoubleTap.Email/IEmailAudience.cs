using JetBrains.Annotations;

namespace DoubleTap.Email
{
    public interface IEmailAudience
    {
        [NotNull]string[] To { get; }
        [CanBeNull]string From { get; }
        bool Targets(string category);
    }
}