using iDeliver.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iDeliver.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext context;

        public RolesController()
        {
            context = new ApplicationDbContext();
        }
        /// <summary>
		/// Get All Roles
		/// </summary>
		/// <returns></returns>
        // GET: Roles
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated && !IsAdminUser())
            {
                return RedirectToAction("Index", "Home");
            }
            
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        private bool IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                return s[0].ToString() == "Admin" ? true : false;
            }
            return false;
        }

        /// <summary>
		/// Create  a New role
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated && !IsAdminUser())
            {
                return RedirectToAction("Index", "Home");
            }
           
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
		/// Create a New Role
		/// </summary>
		/// <param name="Role"></param>
		/// <returns></returns>
		[HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(IdentityRole Role)
        {
            if (!User.Identity.IsAuthenticated && !IsAdminUser())
            {
                return RedirectToAction("Index", "Home");
            }

            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}