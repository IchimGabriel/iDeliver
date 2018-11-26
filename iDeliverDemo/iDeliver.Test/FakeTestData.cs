using iDeliver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDeliver.Test
{
    public class FakeTestData
    {
        static List<Driver> fakeDrivers = new List<Driver>()
       {
           new Driver() { DriverId=1, DriverIdentity="George", Name="George", OnDelivery=false, OnLine=false},
           new Driver() { DriverId=2, DriverIdentity="John", Name="John", OnDelivery=false, OnLine=false},
       };

        static List<Shop> fakeShops = new List<Shop>()
       {
           new Shop() { Name="Pizza", Open=true,  ShopId=1, ShopIdentity="Pizza" },
           new Shop() { Name="Kebab", Open=false,  ShopId=2, ShopIdentity="Kebab" },
       };

        static List<Order> fakeOrders = new List<Order>()
        {
            new Order() { OrderId=1, Address="23 Main Street", Total=55, Commission = 4, DriverIdentity="George", IsDelivered=false, TimeStamp= DateTime.Now, ShopIdentity="Pizza" }

        };

    }



}
