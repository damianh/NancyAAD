namespace NancyAAD.Server
{
    using Microsoft.Owin.Security.ActiveDirectory;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Audience = "https://contoso7.onmicrosoft.com/RichAPI",
                    Tenant = "contoso7.onmicrosoft.com"
                })
                .UseNancy();
        }
    }
}