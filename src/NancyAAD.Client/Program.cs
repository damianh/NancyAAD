namespace NancyAAD.Client
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.WindowsAzure.ActiveDirectory.Authentication;

    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Client ready.");
            Console.WriteLine("Press any key to invoke the service");
            Console.WriteLine("Press ESC to terminate");
            ConsoleKeyInfo consoleKeyInfo;

            var authenticationContext = new AuthenticationContext("https://login.windows.net/contoso7.onmicrosoft.com");

            do
            {
                consoleKeyInfo = Console.ReadKey(true);
                // get the access token
                AuthenticationResult authenticationResult = authenticationContext.AcquireToken(
                    "https://contoso7.onmicrosoft.com/RichAPI",
                    "be182811-9d0b-45b2-9ffa-52ede2a12230",
                    "http://whatevah");
                // invoke the Nancy API
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
                HttpResponseMessage response = httpClient.GetAsync("http://localhost:9000/api/Values").Result;
                // display the result
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("==> Successfully invoked the service");
                    Console.WriteLine(result);
                }
            } while (consoleKeyInfo.Key != ConsoleKey.Escape);
        }
    }
}