using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iDeliver.Models;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace iDeliver.Controllers
{
    public class DriversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders from all shops
        [HttpGet]
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> AllOrders()
        {
            //var orders = db.Orders.Include(o => o.Driver).Include(o => o.Shop);
            var orders = db.Orders;

            return View(await orders.ToListAsync());
        }

        //GET: Orders TO DELIVER current Driver -> OnRoute to customers
        [HttpGet]
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> OnDelivery()
        {
            var user = User.Identity.GetUserId();
            var ondelivery = db.Orders.Where(s => s.DriverIdentity.Length > 1 && s.DriverIdentity.Equals(user));

            return View(await ondelivery.ToListAsync());
        }

        //GET: Orders for current Driver -> Delivered
        [HttpGet]
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> Delivered()
        {
            var user = User.Identity.GetUserId();

            var delivered = db.Orders.Where(s => s.IsDelivered.Equals(true) && s.DriverIdentity.Equals(user));

            return View(await delivered.ToListAsync());
        }

        // GET: Statistics for Driver
        [HttpGet]
        [Authorize(Roles = "Driver")]
        public ActionResult Statistics()
        {
            var user = User.Identity.GetUserId();
            var orders = db.Orders.Where(s => s.DriverIdentity.Equals(user));

            if (orders.Count() == 0)
            {
                ViewBag.Message = "There are no Orders";
            }
            else
            {
                ViewBag.OrdersCount = orders.Count();
                ViewBag.TotalValue = orders.Sum(s => s.Total);
                ViewBag.TotalCommision = orders.Sum(s => s.Commission);
            }

            return View();
        }


        // GET: All Drivers
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Drivers.ToListAsync());
        }

        // GET: Drivers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = await db.Drivers.FindAsync(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }


        // GET: Drivers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = await db.Drivers.FindAsync(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DriverId,Name,OnLine,OnDelivery")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = await db.Drivers.FindAsync(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Driver driver = await db.Drivers.FindAsync(id);
            db.Drivers.Remove(driver);
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
