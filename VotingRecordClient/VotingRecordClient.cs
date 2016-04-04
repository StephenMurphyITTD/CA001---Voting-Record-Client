// Console client for HelloWorld RESTful service
// uses ASP.Net Web API Client API libraries installed in the solution using NuGet
// get hello world greeting and display for a specified name

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VotingRecordClient
{
    class Client
    {
        // start
        static void Main(string[] args)
        {
            Task result = RunAsync();               // convention is for async methods to finish in Async
            result.Wait();                          // block, not the same as await
        }

        static async Task RunAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1194/");                                             // base URL for API Controller i.e. RESFul service
                    client.DefaultRequestHeaders.Accept.Clear();                                                        // add an Accept header 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   // or application/xml or application/json

                    Console.WriteLine("Options Available to Execute: ");
                    Console.WriteLine("1. Query what way everyone voted on a bill ");
                    Console.WriteLine("2. Query what a specific TD voted for in a bill ");
                    Console.WriteLine("3. Query what way a party as a block voted in a bill ");
                    int userChoice = Convert.ToInt32(Console.ReadLine());

                    if (userChoice == 1)
                    {
                        showAll(client);
                    }
                    else if (userChoice == 2)
                    {
                        showTD(client);
                    }
                    else if (userChoice == 3)
                    {
                        showParty(client);
                    }
                    else
                    {
                        Console.WriteLine("Your answer needs to be between 1 - 3");
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        RunAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey(true);
            }
        }

        public static async void showAll(HttpClient client)
        {
            Console.WriteLine("Thanks here you are");
            HttpResponseMessage response = client.GetAsync("api/record").Result;
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                byte[] x = await response.Content.ReadAsByteArrayAsync();
                string message = Encoding.UTF8.GetString(x);
                Console.WriteLine(message);
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
            RunAsync();
        }
        public static async void showTD(HttpClient client)
        {
            Console.WriteLine("Thanks, now which TD would you like to see voting record for?");
            string tdname = Console.ReadLine();
            string tdAPIcall = "bill/name/" + tdname;
            HttpResponseMessage response = client.GetAsync(tdAPIcall).Result;
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                //byte[] x = await response.Content.ReadAsByteArrayAsync();
                //string message = Encoding.UTF8.GetString(x);
                String message = response.Content.ReadAsAsync<string>().Result;                  // accessing the Result property blocks
                Console.WriteLine(message);                                                     // the greeting
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
            RunAsync();
        }

        public static async void showParty(HttpClient client)
        {
            Console.WriteLine("Thanks, now which party would you like to see voting record for?");
            string tdname = Console.ReadLine();
            string tdAPIcall = "bill/party/" + tdname;
            HttpResponseMessage response = client.GetAsync(tdAPIcall).Result;
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                byte[] x = await response.Content.ReadAsByteArrayAsync();
                string message = Encoding.UTF8.GetString(x);
                //String message = response.Content.ReadAsAsync<string>().Result;                  // accessing the Result property blocks
                Console.WriteLine(message);                                                     // the greeting
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
            RunAsync();
        }

        class response
        {
            public string Name { get; set; }
            public string Party { get; set; }
            public string Vote { get; set; }
        }
    }
}