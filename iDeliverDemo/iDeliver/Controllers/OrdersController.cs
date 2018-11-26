using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iDeliver.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace iDeliver.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        [Authorize(Roles = "Admin, Analyst, ShopMng")]
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.Driver).Include(o => o.Shop);
            return View(await orders.ToListAsync());
        }

        //GET: Orders for current shop
        [Authorize(Roles = "ShopMng")]
        public async Task<ActionResult> IndexShopMng()
        {
            var user = User.Identity.GetUserId();
            var orders = db.Orders.Select(s => s.ShopIdentity.Equals(user));

            //var orders = db.Orders.ToListAsync().Result.FindAll(s => s.ShopIdentity.Equals(user));
            //orders.OrderByDescending(t => t.TimeStamp);
           
            return View(await orders.ToListAsync());
        }


        // GET: Orders/Details/5
        [Authorize(Roles = "ShopMng , Driver")]
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

        // GET: Orders/Create
        [Authorize(Roles = "ShopMng")]
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverIdentity", "Name");
            ViewBag.ShopId = new SelectList(db.Shops, "ShopIdentity", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        [Authorize(Roles = "Admin, Driver")]
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
        [Authorize(Roles = "Admin, Driver")]
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
