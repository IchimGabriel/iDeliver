using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDeliver.Models
{
    public class Shop 
    {
        public Shop()
        {
            Open = false;
        }

        [Key]
        public int ShopId { get; set; }

        public string Name { get; set; }

        public bool Open { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}