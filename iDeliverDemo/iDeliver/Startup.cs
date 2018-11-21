using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using iDeliver.Models;
using Owin;


namespace iDeliver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRolesAndUsers();
        }

        //private void CreateRolesAndUsers()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //    // In Startup iam creating first Admin Role and creating a default Admin User 
        //    if (!RoleManager.RoleExists("Admin"))
        //    {

        //        // first we create Admin rool
        //        var role = new IdentityRole
        //        {
        //            Name = "Admin"
        //        };
        //        RoleManager.Create(role);

        //        //Here we create a Admin super user who will maintain the website				

        //        var user = new ApplicationUser
        //        {
        //            UserName = "Gabriel",
        //            Email = "gabrielichim@codexapi.com"
        //        };

        //        string userPass = "V**********##";

        //        var checkUser = UserManager.Create(user, userPass);

        //        //Add default User to Role Admin
        //        if (checkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "Admin");
        //        }
        //    }

        //    // create Shop Manager role 
        //    if (!RoleManager.RoleExists("ShopMng"))
        //    {
        //        var role = new IdentityRole
        //        {
        //            Name = "ShopMng"
        //        };
        //        RoleManager.Create(role);
        //    }

        //    // create Driver role 
        //    if (!RoleManager.RoleExists("Driver"))
        //    {
        //        var role = new IdentityRole
        //        {
        //            Name = "Driver"
        //        };
        //        RoleManager.Create(role);
        //    }

        //    // create Driver role 
        //    if (!RoleManager.RoleExists("Analyst"))
        //    {
        //        var role = new IdentityRole
        //        {
        //            Name = "Analyst"
        //        };
        //        RoleManager.Create(role);
        //    }
        //}
    }
}
