using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iDeliver.Models
{
    public class Driver 
    {
        public Driver()
        {
            OnLine = false;
            OnDelivery = false;
        }


        [Key]
        public int DriverId { get; set; }

        public string Name { get; set; }

        public bool OnLine { get; set; }

        public bool OnDelivery { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}