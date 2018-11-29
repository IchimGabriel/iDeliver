using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iDeliver.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Collections.Generic;

namespace iDeliver.Controllers
{
    public class OrdersController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// ORDERS ISSUED BY CURRENT SHOP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "ShopMng")]
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.GetUserId();
            var orders = db.Orders
                .Where(s => s.ShopIdentity.Equals(user))
                .OrderByDescending(t => t.TimeStamp);

            var shop = db.Shops
                .Where(s => s.ShopIdentity.Equals(user))
                .ToList();

            var isOpen = shop[0].Open;
           
            ViewBag.Open = isOpen;
     
            return View(await orders.ToListAsync());
        }

        /// <summary>
        /// CURRENT SHOP ORDERS -> ON ROUTE TO CUSTOMERS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "ShopMng")]
        public async Task<ActionResult> OnDelivery()
        {
            var user = User.Identity.GetUserId();

            var ondelivery = db.Orders
                .Where(s => s.DriverIdentity.Length > 1 && s.ShopIdentity.Equals(user))
                .OrderByDescending(t => t.TimeStamp);

            return View(await ondelivery.ToListAsync());
        }

        /// <summary>
        /// CURRENT SHOP ORDERS -> DELIVERED
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "ShopMng")]
        public async Task<ActionResult> Delivered()
        {
            var user = User.Identity.GetUserId();

            var delivered = db.Orders.Where(s => s.IsDelivered.Equals(true) && s.ShopIdentity.Equals(user));

            return View(await delivered.ToListAsync());
        }

        /// <summary>
        /// ORDERS FROM ALL SHOPS -> ADMIN AUTHORIZATION
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "ShopMng")]
        public ActionResult Statistics()
        {
            var user = User.Identity.GetUserId();
            var orders = db.Orders.Where(s => s.ShopIdentity.Equals(user));

            if (orders.Count() == 0)
            {
                ViewBag.Message = "There are no Orders";
            }
            else
            {
                ViewBag.OrdersCount = orders.Count();
                ViewBag.TotalValue = orders.Sum(s => s.Total);
                ViewBag.TotalCommision = orders.Sum(s => s.Commission);
                ViewBag.ShopTotal = orders.Sum(s => s.Total) - orders.Sum(s => s.Commission);
            }

            return View();
        }

        /// <summary>
        /// GET: CREATE ORDERS -> ORDERS/CREATE
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "ShopMng")]
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverIdentity", "Name");
            ViewBag.ShopId = new SelectList(db.Shops, "ShopIdentity", "Name");
            return View();
        }

        /// <summary>
        /// POST: CREATE ORDERS -> ORDERS/CREATE
        /// </summary>
        /// <param name="order"></param>
        /// <returns>CREATE NEW ORDER</returns>
        [HttpPost]
        [Authorize(Roles = "ShopMng")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,TimeSpan,Total,Commission,Address,IsDelivered,DriverIdentity,ShopIdentity")] Order order)
        {
            order.ShopIdentity = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.Drivers, "DriverIdentity", "Name", order.DriverIdentity);
            ViewBag.ShopId = new SelectList(db.Shops, "ShopIdentity", "Name", order.ShopIdentity);
            return View(order);
        }


        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverIdentity", "Name", order.DriverIdentity);
            ViewBag.ShopId = new SelectList(db.Shops, "ShopIdentity", "Name", order.ShopIdentity);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,TimeSpan,Total,Commission,Address,IsDelivered,DriverIdentity,ShopIdentity")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverIdentity", "Name", order.DriverIdentity);
            ViewBag.ShopId = new SelectList(db.Shops, "ShopId", "Name", order.ShopIdentity);
            return View(order);
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
