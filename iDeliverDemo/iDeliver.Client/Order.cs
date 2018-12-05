using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDeliver.Client
{
    class Order
    {
 
        public int OrderId { get; set; }

        public DateTime TimeStamp { get; set; } 
        
        public decimal Total { get; set; }
        
        public decimal Commission { get; set; }
      
        public string Address { get; set; }

        public bool IsDelivered { get; set; }

        public string DriverIdentity { get; set; }

        public string ShopIdentity { get; set; }

    }
}
