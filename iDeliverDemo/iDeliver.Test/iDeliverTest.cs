using iDeliver.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace iDeliver.Test
{
    [TestClass()]
    public class iDeliverTest
    {
        private decimal totalTest = 0;

        [TestMethod()]
        public void DriverTest()
        {
            //arrange and act
            Driver dr = new Driver() { DriverId = 1000, DriverIdentity = "GG", Name = "George Green", OnDelivery = true, OnLine = true };
            Assert.IsInstanceOfType(dr, typeof(Driver));
            Assert.AreEqual(dr.DriverId,1000); 
            Assert.AreEqual(dr.Name, "George Green");
            Assert.AreEqual(dr.OnDelivery, true);
        }

        [TestMethod()]
        public void OrderTest()
        {
            Order or = new Order() { Address = "Test Street", IsDelivered = true };

            //assert
            Assert.IsInstanceOfType(or, typeof(Order));
            Assert.AreEqual(or.Address, "Test Street");
            Assert.AreNotEqual(or.IsDelivered, false);

        }

        [TestMethod()]
        public void ShopTest()
        {
            var sh = new Shop() { Name = "Royal Pizza", ShopId = 23 };

            //assert
            Assert.IsInstanceOfType(sh, typeof(Shop));
            Assert.AreEqual(sh.Name, "Royal Pizza");
            Assert.AreNotEqual(sh.ShopId, 12);

        }

       
        [TestMethod]
        public void GetSumTest()
        {
            foreach (var item in FakeTestData.fakeOrders)
            {
                totalTest += item.Total;
            }

            Assert.AreEqual(totalTest, GetTotalSum(FakeTestData.fakeOrders));

        }

        public decimal GetTotalSum(List<Order> orders)
        {
            decimal totalsum = 0;

            foreach (var item in orders)
            {
                totalsum += item.Total;

            }

            return totalsum;

        }

        


    }

}






    
