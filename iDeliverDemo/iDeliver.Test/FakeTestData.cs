using iDeliver.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iDeliver.Models;

namespace iDeliver.Test
{
    [TestClass()]
    public class iDeliverTest
    {
        [TestMethod()]
        public void DriverTest()
        {
            Driver dr = new Driver() { DriverId = 1000, DriverIdentity = "GG", Name = "George Green", OnDelivery = true, OnLine = true };
            Assert.IsInstanceOfType(dr, typeof(Driver));
            Assert.AreEqual(dr.DriverId, 1000);
            Assert.AreEqual(dr.Name, "George Green");
            Assert.AreEqual(dr.OnDelivery,true);
            
            
            //List<Driver> testDriver = new List<Driver>()
            // {
            // new Driver() { DriverId=1, DriverIdentity="George", Name="George", OnDelivery=false, OnLine=false},
            // Assert.AreEqual(testDriver.DriverID)


            // };
             //new Driver() { DriverId=2, DriverIdentity="John", Name="John", OnDelivery=false, OnLine=false},
             
        }
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
