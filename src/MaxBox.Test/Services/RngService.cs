using System;
using System.Linq;
using MaxBox.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaxBox.Test.Services
{
    [TestClass]
    public class RngServiceUnitTest
    {
        private IRngService _rngService;

        [TestInitialize]
        public void Init()
        {
            _rngService = new RngService();
        }

        [TestMethod]
        public void CheckGeneratedStringLength()
        {
            string tenlengthstring = _rngService.GenerateString(10);
            Assert.AreEqual(tenlengthstring.Count(), 10);
        }

        [TestMethod]
        public void AreThe2StringsTheSame()
        {
            string firststring = _rngService.GenerateString(5);
            string secondstring = _rngService.GenerateString(5);
            Assert.AreNotEqual(firststring, secondstring);
        }

        [TestMethod]
        public void CheckOrOnlyNumerHasNoOtherChars()
        {
            string onlynumeric = _rngService.GenerateString(5, false, false, numeric: true);
            int numeric = Convert.ToInt32(onlynumeric);
            Assert.IsInstanceOfType(numeric, typeof (int));
        }
    }
}