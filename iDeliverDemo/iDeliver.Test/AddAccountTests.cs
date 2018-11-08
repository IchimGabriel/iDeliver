using System;
using iDeliver.Controllers;
using iDeliver.Models;
using Xunit;
using Moq;

namespace iDeliver.Test
{
    
    public class AddAccountTests
    {
        [Fact]
        public void TestMethod1()
        {
            // arange
            var mock = new Mock<SendCodeViewModel>();
            mock.SetupSet(s => s.ReturnUrl = "http://abc.com");

            var testString = new SendCodeViewModel();
            testString.ReturnUrl.Contains("http://");

            // act


            var result = mock.Equals(testString);

            // assert

            Assert.False(result);
        }
    }
}
