namespace NancyAAD.Server
{
    using System;
    using Microsoft.Owin.Hosting;

    internal class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:9000/"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Nancy listening at http://localhost:9000/");

                // Test call
                /*var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("http://localhost:9000/values").Result;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);*/

                Console.WriteLine("Press ENTER to terminate");
                Console.ReadLine(); 
            }
        }
    }
}