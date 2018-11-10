using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDeliver.Models
{
    public class Order
    {
        public Order()
        {
            DateTime = DateTime.Now;
            IsDelivered = false;
        }

        [Key]
        public int OrderId { get; set; }

        public DateTime DateTime { get; set; } 

        [Display(Name = "Total")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Total { get; set; }

        [Display(Name = "Driver Commision")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Commission { get; set; }

        [Display(Name = "Delivery Address")]
        public string Address { get; set; }

        public bool IsDelivered { get; set; }

        public int? DriverId { get; set; }

        [Required]
        public int ShopId { get; set; }


        public virtual Driver Driver { get; set; }
        public virtual Shop Shop { get; set; }
    }
}