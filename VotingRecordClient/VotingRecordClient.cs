// Console client for HelloWorld RESTful service
// uses ASP.Net Web API Client API libraries installed in the solution using NuGet
// get hello world greeting and display for a specified name

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.IO;

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
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://votingrecord.azurewebsites.net");                              // base URL for API Controller i.e. RESFul service
                //client.BaseAddress = new Uri("http://localhost:56174");
                client.DefaultRequestHeaders.Accept.Clear();                                                        // add an Accept header 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   // or application/xml or application/json

                Console.WriteLine(client.BaseAddress);
                Console.WriteLine("Getting All Records for this bill...");
                Console.ReadKey();
                showAll(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Getting Record for Enda Kenny...");
                Console.ReadKey();
                showTD(client);
                Console.WriteLine(" ");
                Console.ReadKey();


                Console.WriteLine("Getting Record for Sinn Fein...");
                Console.ReadKey();
                showParty(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Getting Record for all Absent TDs...");
                Console.ReadKey();
                showVote(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Inserting Stephen Murphy (Party: Independent, Vote: Nil) to DB...");
                Console.ReadKey();
                insertRecord(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Getting Record for Stephen Murphy...");
                showSteMur(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Updating Stephen Murphy to Vote Ta in the DB...");
                Console.ReadKey();
                updateRecord(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Getting Record for Stephen Murphy...");
                showSteMur(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Deleting Stephen Murphy from the DB entirely...");
                Console.ReadKey();
                deleteRecord(client);
                Console.WriteLine(" ");
                Console.ReadKey();

                Console.WriteLine("Getting Record for Stephen Murphy...");
                showSteMur(client);
                Console.WriteLine(" ");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey(true);
            }
        }

        public static void showAll(HttpClient client)
        {
            HttpResponseMessage response = client.GetAsync("BillNo/All").Result;
            
            if (response.IsSuccessStatusCode)
            {

                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
        }
        public static void showTD(HttpClient client)
        {
            string name = "Enda";
            string surname = "Kenny";
            string tdAPIcall = "billNo/TD/" + name + "/" + surname;
            HttpResponseMessage response = client.GetAsync(tdAPIcall).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
        }

        public static void showParty(HttpClient client)
        {
            string party = "SF";
            string partyAPIcall = "billNo/Party/" + party;
            HttpResponseMessage response = client.GetAsync(partyAPIcall).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
        }

        public static void showVote(HttpClient client)
        {
            string vote = "Absent";
            string VoteAPIcall = "billNo/Vote/" + vote;
            HttpResponseMessage response = client.GetAsync(VoteAPIcall).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
        }

        public static void insertRecord(HttpClient client)
        {
            string URI = "http://votingrecord.azurewebsites.net/BillNo/Insert/Stephen/Murphy/Independent/Nil";
            string data = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
            request.Method = "POST";
            request.ContentType = "text/plain";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.WriteLine(data);
            }

            WebResponse response = request.GetResponse();
            if (response.ContentLength == 0)
                Console.WriteLine("OK");
            else
                Console.WriteLine(response);

        }

        public static void updateRecord(HttpClient client)
        {
            string URI = "http://votingrecord.azurewebsites.net/BillNo/Update/Stephen/Murphy/Ta";
            string data = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
            request.Method = "PUT";
            request.ContentType = "text/plain";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.WriteLine(data);
            }

            WebResponse response = request.GetResponse();
            if (response.ContentLength == 0)
                Console.WriteLine("OK");
            else
                Console.WriteLine(response);

        }
        public static void deleteRecord(HttpClient client)
        {
            string URI = "http://votingrecord.azurewebsites.net/BillNo/Delete/Stephen/Murphy";
            string data = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
            request.Method = "DELETE";
            request.ContentType = "text/plain";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.WriteLine(data);
            }

            WebResponse response = request.GetResponse();
            if (response.ContentLength == 0)
                Console.WriteLine("OK");
            else
                Console.WriteLine(response);
        }

        public static void showSteMur(HttpClient client)
        {
            string name = "Stephen";
            string surname = "Murphy";
            string tdAPIcall = "billNo/TD/" + name + "/" + surname;
            HttpResponseMessage response = client.GetAsync(tdAPIcall).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                Console.ReadKey(true);
            }
        }
    }
}