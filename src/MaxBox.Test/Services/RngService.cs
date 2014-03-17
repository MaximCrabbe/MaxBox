using System;
using System.Linq;
using MaxBox.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaxBox.Test.Services
{
    [TestClass]
    public class RngServiceUnitTest
    {
        [TestMethod]
        public void GenerateString()
        {
            var service = new RngService();
            
            var tenlengthstring = service.GenerateString(10);
            Assert.AreEqual(tenlengthstring.Count(), 10);

            var firststring = service.GenerateString(5);
            var secondstring = service.GenerateString(5);
            Assert.AreNotEqual(firststring, secondstring);

            var onlynumeric = service.GenerateString(5, false, true);
            int numeric = Convert.ToInt32(onlynumeric);
            Assert.IsInstanceOfType(numeric, typeof(int));
        }
    }
}