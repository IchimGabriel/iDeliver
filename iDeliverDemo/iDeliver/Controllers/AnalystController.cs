using iDeliver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace iDeliver.Controllers
{
    public class AnalystController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> CreateBar()
        {
            //Create bar chart
            var data = db.Orders.Where(s => s.ShopIdentity != null).OrderByDescending(s => s.Total).ToList();
            List<int> id = new List<int>();
            List<decimal> total = new List<decimal>();

            foreach (var item in data)
            {
                id.Add(item.OrderId);
                total.Add(item.Total);
            }
            var myChart = new Chart(width: 900, height: 500, theme: ChartTheme.Blue)
                .AddTitle("Orders")
                .AddSeries("Orders",
                    xValue: id, xField: "Id",
                    yValues: total, yFields: "Total")
                .AddLegend()
                .SetXAxis("Order ID", 1)
                .SetYAxis("Value", 0, 100)
                .Write()
                .GetBytes("png");
           
            return File(myChart, "image/bytes");
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}