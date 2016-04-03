// Console client for HelloWorld RESTful service
// uses ASP.Net Web API Client API libraries installed in the solution using NuGet
// get hello world greeting and display for a specified name

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

                    HttpResponseMessage response = await client.GetAsync("bill/party/Labour");
                    Console.WriteLine(response.IsSuccessStatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        // parse result 
                        response message = await response.Content.ReadAsAsync<response>();                  // accessing the Result property blocks
                        Console.WriteLine("Stephen Was Also Here");
                        //Console.WriteLine("{0}\t${1}\t{2}", response.Name, response.Party, response.Vote);                                                    // the greeting
                        response deserializedProduct = JsonConvert.DeserializeObject<response>(message);
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