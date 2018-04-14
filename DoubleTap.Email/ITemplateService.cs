using JetBrains.Annotations;

namespace DoubleTap.Email
{
    [PublicAPI]
    public interface ITemplateService
    {
        string Apply<T>(string templateKey, T model);
    }
}