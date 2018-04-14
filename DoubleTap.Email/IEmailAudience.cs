using JetBrains.Annotations;

namespace DoubleTap.Email
{
    public interface IEmailAudience
    {
        [NotNull]string[] To { get; }
        [CanBeNull] From From { get; }
        bool Targets(string category);
    }
}