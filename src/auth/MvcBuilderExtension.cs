using Microsoft.Extensions.DependencyInjection;
using QFile.AuthenticationServer;

namespace Bodegas.Auth
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddAuthenticationViewLocationExpander(this IMvcBuilder builder)
        {
            return builder.AddRazorOptions(razor =>
            {
                razor.ViewLocationExpanders.Add(new QFileViewLocationExpander());
            });
        }
    }
}
