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

                    Console.WriteLine("Do you want to see results from a party or from a TD? (Enter party or name): ");
                    string partyorTD = Console.ReadLine();
                    Console.WriteLine(partyorTD);

                    Console.WriteLine("thanks!, now please enter the party or TD name you wish to review: ");
                    string name = Console.ReadLine();

                    string APICall = "bill/" + partyorTD + "/" + name;
                    Console.WriteLine(APICall);

                    //HttpResponseMessage response = client.GetAsync("bill/party/SinnFein").Result;
                    HttpResponseMessage response = client.GetAsync(APICall).Result;
                    Console.WriteLine(response.IsSuccessStatusCode);
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey(true);
            }
        }

        class response
        {
            public string Name { get; set; }
            public string Party { get; set; }
            public string Vote { get; set; }
        }
    }
}