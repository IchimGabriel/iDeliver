using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
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
                myClient = new HttpClient{ BaseAddress = new Uri("https://idelivernow.azurewebsites.net/")};
                myClient.DefaultRequestHeaders.Accept.Clear();
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create HttpClient - program exiting");
                return;
            }

            GetAllOrdersSumAmount(myClient).Wait();
            Console.ReadLine();
        }

        //	Return data about ..................
         static async Task GetAllOrdersSumAmount(HttpClient myClient)
        {
            try
            {
                HttpResponseMessage response = await myClient.GetAsync("api/orders");  
                if (response.IsSuccessStatusCode)
                {
                   
                    var orders = await response.Content.ReadAsAsync<IEnumerable<Order>>();
                    var sorted = orders.OrderByDescending(s => s.OrderId);
                    
                    Console.WriteLine("\n\t Order List Desc:");
                    foreach (var c in sorted)
                    {      
                        Console.WriteLine("\n\t" + c.OrderId + " \t Sum: " + c.Total + "\t| " + c.Address);
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
        }
    }
}
    