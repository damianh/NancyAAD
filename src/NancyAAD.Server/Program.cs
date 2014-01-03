namespace NancyAAD.Server
{
    using System;
    using System.Security.Claims;
    using Microsoft.Owin.Hosting;
    using Microsoft.Owin.Security.ActiveDirectory;
    using Nancy;
    using Nancy.Security;
    using Owin;

    internal class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:9000/"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Nancy listening at http://localhost:9000/");

                /*// Test call
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("http://localhost:9000/api/values").Result;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);*/

                Console.ReadLine();
            }
        }
    }

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

    public class ValuesModule : NancyModule
    {
        public ValuesModule() : base("api")
        {
            this.RequiresOwinAuthentication();
            Get["/values"] = _ =>
            {
                ClaimsPrincipal claimsPrincipal = Context.GetAuthenticationManager().User;
                Console.WriteLine("==>I have been called by {0}", claimsPrincipal.FindFirst(ClaimTypes.Upn));
                return new[] {"value1", "value2"};
            };
        }
    }
}