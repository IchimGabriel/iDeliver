using iDeliver.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Xml;
using System.Web.Helpers;
using System.Web.Mvc;

namespace iDeliver.Controllers
{
    public class AnalystController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        // GET: Analyst
        [HttpGet]
        public ActionResult Index()
        {

            var data = db.Orders.Where(s => s.ShopIdentity != null).OrderByDescending(s => s.Total).ToList();
            List<int> id = new List<int>();
            List<decimal> total = new List<decimal>();

            foreach (var item in data)
            {
                    id.Add(item.OrderId);
                    total.Add(item.Total);
            }
            var myChart = new Chart(width: 800, height: 600, theme: ChartTheme.Green)
                .AddTitle("Orders")
                .AddSeries("All Orders",
                    xValue: id, xField: "Id",
                    yValues: total, yFields: "Total")
                .AddLegend()
                .Write();

            ///ViewBag.Chart = myChart;

            return View();
        }
    }
}