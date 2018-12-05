using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace iDeliver.Client
{

    class Program
    {
        static void Main(string[] args)
        {
            HttpClient myClient = null;

            try
            {
                myClient = new HttpClient();
                myClient.BaseAddress = new Uri("https://idelivernow.azurewebsites.net/");
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create HttpClient - program exiting");
                return;
            }

            GetAllOrdersSumAmount(myClient).Wait();
            //GetAllDeploymentsAsync(myClient).Wait();
            //GetClientDeploymentByClientIDAsync(myClient).Wait();
            //GetClientDeploymentByClientNameAsync(myClient).Wait();
            //GetClientsUsingServerAsync(myClient).Wait();

            Console.ReadLine();

        }

        //	Return data about ..................
        private static async Task GetAllOrdersSumAmount(HttpClient myClient)
        {
            try
            {
                HttpResponseMessage response = await myClient.GetAsync("api/Sum");
                if (response.IsSuccessStatusCode)
                {
                    // read results 
                    var clients = await response.Content.ReadAsAsync<IEnumerable<Order>>();
                    foreach (var c in clients)
                    {
                        var total = c.Total.ToString();
                        Console.WriteLine(total);
                    }
                }
                else
                {
                    Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }

        ////2.	Return data about all deployments.
        //private static async Task GetAllDeploymentsAsync(HttpClient myClient)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await myClient.GetAsync("api/Deployments");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // read results 
        //            var deployments = await response.Content.ReadAsAsync<IEnumerable<Deployment>>();
        //            foreach (var d in deployments)
        //            {
        //                Console.WriteLine(d.ClientAppURL);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        ////3.	Return data about a client’s deployments as specified using a client ID
        //private static async Task GetClientDeploymentByClientIDAsync(HttpClient myClient)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await myClient.GetAsync("api/Deployments/0");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // read results 
        //            var deployments = await response.Content.ReadAsAsync<IEnumerable<Deployment>>();
        //            foreach (var d in deployments)
        //            {
        //                Console.WriteLine(d.ClientAppURL);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        ////4.	Return data about a client’s deployments as specified using a client name
        //private static async Task GetClientDeploymentByClientNameAsync(HttpClient myClient)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await myClient.GetAsync("api/Deployments/GetClientDeployments/JemmyNed");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // read results 
        //            var deployments = await response.Content.ReadAsAsync<IEnumerable<Deployment>>();
        //            foreach (var d in deployments)
        //            {
        //                Console.WriteLine(d.ClientAppURL);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        ////5.	Return a list of the clients using a specific server instance.
        //private static async Task GetClientsUsingServerAsync(HttpClient myClient)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await myClient.GetAsync("api/Deployments/GetClientsonServer/LinuxApache01");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // read results 
        //            var clients = await response.Content.ReadAsAsync<IEnumerable<Client>>();
        //            foreach (var c in clients)
        //            {
        //                Console.WriteLine(c.ClientName);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        // }
    }
}
    