using iDeliver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace iDeliver.Controllers
{
    /// <summary>
    /// WebAPIController 
    /// </summary>
    [RoutePrefix("api")]
    public class WebAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// GetUsers()
        /// </summary>
        /// <returns> All current Users in application</returns>
        [Route("users")]
        [HttpGet]
        public IEnumerable<ApplicationUser> GetUsers()
        {
            return db.Users.ToList();
        }

        /// <summary>
        /// GetUserWithId()
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User with Id number selected</returns>
        [Route("user/{id}")]
        [HttpGet]
        public ApplicationUser GetUserWithId(string id)
        {
            var user = db.Users.FirstOrDefault(s => s.Id.Equals(id));

            return user;
        }

        /// <summary>
        /// GetOrders()
        /// </summary>
        /// <returns></returns>
        [Route("orders")]
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            IOrderedQueryable<Order> orders = db.Orders.OrderBy(s => s.ShopIdentity);

            return orders.ToList();
        }

        /// <summary>
        /// GetSum()
        /// </summary>
        /// <returns></returns>
        [Route("sum")]
        [HttpGet]
        public decimal GetSum()
        {
            var sum = db.Orders.Sum(s => s.Total);

            return sum;
        }
    }
}