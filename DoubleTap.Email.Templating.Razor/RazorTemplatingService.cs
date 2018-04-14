using System;
using RazorEngine.Templating;

namespace DoubleTap.Email.Templating.Razor
{
    public class RazorTemplatingService : ITemplateService
    {
        readonly IRazorEngineService _razor;

        public RazorTemplatingService(IRazorEngineService razor)
        {
            _razor = razor;
        }

        public string Apply<T>(string templateKey, T model)
        {
            return _razor.RunCompile(templateKey, null, model);
        }
    }
}
