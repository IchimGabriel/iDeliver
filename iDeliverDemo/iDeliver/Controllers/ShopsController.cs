using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using iDeliver.Models;
using Microsoft.AspNet.Identity;
using System.Linq;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShopClose([Bind(Include = "ShopId,ShopIdentity,Name,Open")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                var meshop = db.Shops.Where(s => s.ShopIdentity.Equals(user)).ToList();
                var meid = meshop[0].ShopId;
                var name = meshop[0].Name;

                var currentshop = db.Shops.Find(meid);

                currentshop.ShopIdentity = user;
                currentshop.Name = name;
                currentshop.Open = false;


                db.Entry(currentshop).State = EntityState.Modified;
                await db.SaveChangesAsync(); 
                return RedirectToAction("Index", "Orders");
            }

            return RedirectToAction("Index", "Orders");
        }

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShopOpen([Bind(Include = "ShopId,ShopIdentity,Name,Open")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                var meshop = db.Shops.Where(s => s.ShopIdentity.Equals(user)).ToList();
                var meid = meshop[0].ShopId;
                var name = meshop[0].Name;

                var currentshop = db.Shops.Find(meid);

                currentshop.ShopIdentity = user;
                currentshop.Name = name;
                currentshop.Open = true;


                db.Entry(currentshop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }

            return RedirectToAction("Index", "Orders");
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
