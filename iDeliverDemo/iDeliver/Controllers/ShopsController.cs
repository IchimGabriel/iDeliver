using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iDeliver.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Collections.Generic;
using System;

namespace iDeliver.Controllers
{
    public class ShopsController : Controller
    {
        private ApplicationDbContext db;

        public ShopsController()
        {
            db = new ApplicationDbContext();
        }

        // GET: All Shops
        [Authorize(Roles = "Analyst, Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Shops.ToListAsync());
        }

        // GET: Shops/Edit/5
        [Authorize(Roles = "ShopMng")]
        public async Task<ActionResult> Edit(int? id)
        {
            var user = User.Identity.GetUserId();
            var meshop = db.Shops.Where(s => s.ShopIdentity.Equals(user)).ToList();
            id = meshop[0].ShopId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            Shop shop = await db.Shops.FindAsync(id);
            if (shop == null)
            {
                return HttpNotFound();
            }

            return View(shop);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "ShopMng")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ShopId,Name,Open")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                shop.ShopIdentity = user;

                db.Entry(shop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }
            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShoOff()
        {
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShopOn()
        {
            
            return RedirectToAction("Index", "Home");
        }

        // GET: Shops/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = await db.Shops.FindAsync(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // GET: Shops/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = await db.Shops.FindAsync(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: Shops/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shop shop = await db.Shops.FindAsync(id);
            db.Shops.Remove(shop);
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
