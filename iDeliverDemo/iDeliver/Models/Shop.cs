using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iDeliver.Models
{
    public class Shop 
    {
        public Shop()
        {
            Open = false;
        }

       [Key]
        public int Id { get; set; }

        public string ShopIdentity { set; get; }

        public string Name { get; set; }

        public bool Open { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}