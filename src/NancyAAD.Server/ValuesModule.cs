namespace NancyAAD.Server
{
    using System;
    using System.Security.Claims;
    using Nancy;
    using Nancy.Security;

    public class ValuesModule : NancyModule
    {
        public ValuesModule()
        {
            this.RequiresMSOwinAuthentication();
            Get["/values"] = _ =>
            {
                ClaimsPrincipal claimsPrincipal = Context.GetAuthenticationManager().User;
                Console.WriteLine("==>I have been called by {0}", claimsPrincipal.FindFirst(ClaimTypes.Upn));
                return new[] {"value1", "value2"};
            };
        }
    }
}