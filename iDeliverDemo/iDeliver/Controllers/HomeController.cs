using iDeliver.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace iDeliver.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(RegisterViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Guest");
            }

            var user = User.Identity;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            var s = UserManager.GetRoles(user.GetUserId());

            ViewResult view;

            switch (s[0])
            {
                case "Admin":
                    view = View("Index", "_AdminLayout");
                    break;
                case "Driver":
                    view = View("Index", "_DriverLayout");
                    break;
                case "ShopMng":
                    view = View("Index", "_ShopLayout");
                    break;
                case "Analyst":
                    view = View("Index", "_AnaystLayout");
                    break;
                default:
                    view = View("Index", "_Layout");
                    break;
            }
           
            return view;
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Guest()
        {
            return View();
        }
    }
}