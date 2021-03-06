﻿using System.Data.Entity;
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

        /// <summary>
        /// ORDERS FROM ALL SHOPS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> AllOrders()
        {
            var orders = db.Orders.Where(s => s.DriverIdentity == null);

            var user = User.Identity.GetUserId();
            var driver = db.Drivers
                .Where(s => s.DriverIdentity.Equals(user))
                .ToList();

            var isOnline = driver[0].OnLine;

            ViewBag.Online = isOnline;

            return View(await orders.ToListAsync());
        }

        /// <summary>
        /// ORDERS ON ROUTE TO CUSTOMERS FOR CURRENT DRIVER
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> OnDelivery()
        {
            var user = User.Identity.GetUserId();
            var ondelivery = db.Orders.Where(s => s.DriverIdentity.Length > 1 && s.DriverIdentity.Equals(user) && s.IsDelivered == false);

            return View(await ondelivery.ToListAsync());
        }

        /// <summary>
        /// DELIVERED ORDERS FOR CURRENT DRIVER
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> Delivered()
        {
            var user = User.Identity.GetUserId();

            var delivered = db.Orders.Where(s => s.IsDelivered.Equals(true) && s.DriverIdentity.Equals(user));

            return View(await delivered.ToListAsync());
        }

        /// <summary>
        /// GET STATISTICS FOR DRIVER
        /// </summary>
        /// <returns></returns>   
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

        /// <summary>
        /// PUT THE DRIVER IN OFFLINE MODE
        /// </summary>
        /// <param name="driver"> OnLine= false;</param>
        /// <returns>CHANGE STATUS FROM TRUE TO FALSE</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DriverOffline([Bind(Include = "DriverId,DriverIdentity,Name,OnLine,OnDelivery")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                var medriver = db.Drivers.Where(s => s.DriverIdentity.Equals(user)).ToList();
                var meid = medriver[0].DriverId;
                var name = medriver[0].Name; 

                var currentdriver = db.Drivers.Find(meid);

                currentdriver.DriverIdentity = user;
                currentdriver.Name = name;
                currentdriver.OnLine = false;

                db.Entry(currentdriver).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AllOrders", "Drivers");
            }

            return RedirectToAction("AllOrders", "Drivers");
        }

        /// <summary>
        /// PUT THE DRIVER IN ONLINE MODE
        /// </summary>
        /// <param name="driver"> OnLine= true;</param>
        /// <returns>CHANGE STATUS FROM FALSE TO TRUE</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DriverOnline([Bind(Include = "DriverId,DriverIdentity,Name,OnLine,OnDelivery")] Driver driver)
        {
            if (ModelState.IsValid)  
            {
                var user = User.Identity.GetUserId();
                var medriver = db.Drivers.Where(s => s.DriverIdentity.Equals(user)).ToList();
                var meid = medriver[0].DriverId;
                var name = medriver[0].Name;

                var currentdriver = db.Drivers.Find(meid);

                currentdriver.DriverIdentity = user;
                currentdriver.Name = name;
                currentdriver.OnLine = true;

                db.Entry(currentdriver).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AllOrders", "Drivers");
            }

            return RedirectToAction("AllOrders", "Drivers");
        }

        /// <summary>
        /// ORDER TAKEN BY DRIVER
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Orders/Edit/5
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> AddDriver(int? id, Order order)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentorder = await db.Orders.FindAsync(id);
            if (currentorder == null)
            {
                return HttpNotFound();
            }
            
            var user = User.Identity.GetUserId();
            currentorder.DriverIdentity = user;

            if (ModelState.IsValid)
            {
                db.Entry(currentorder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("OnDelivery", "Drivers");
            }

            return RedirectToAction("OnDelivery", "Drivers");
        }

        /// <summary>
        /// ORDER DELIVERED
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Driver")]
        public async Task<ActionResult> OrderDelivered(int? id, Order order)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            var currentorder = await db.Orders.FindAsync(id);
            if (currentorder == null)
            {
                return HttpNotFound();
            }

            var user = User.Identity.GetUserId();
            //order.DriverIdentity = user;
            currentorder.IsDelivered = true;

            if (ModelState.IsValid)
            {
                db.Entry(currentorder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Delivered", "Drivers");
            }

            return RedirectToAction("Delivered", "Drivers");
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
