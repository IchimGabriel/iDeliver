using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDeliver.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        public string Name { get; set; }

        public bool OnLine { get; set; }

        public bool OnDelivery { get; set; }

        public bool Offline { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}