using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MaxBox.AssemblyDummy;
using MaxBox.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaxBox.Test.Helpers
{
    [TestClass]
    public class AssemblyHelperUnitTest
    {
        private IAssemblyHelper _assemblyHelper;

        [TestInitialize]
        public void Init()
        {
            _assemblyHelper = new AssemblyHelper();
        }

        [TestMethod]
        public void CheckOrAllClassesGetLoadedByInterface()
        {
            IEnumerable<Type> classes =
                _assemblyHelper.GetAllClassesWithInterface<ISimpleInterface>("MaxBox.Test.AssemblyDummy");
            Assert.IsTrue(classes.Any(x => x.Name == "Class1WithSimpleInterface"));
            Assert.IsTrue(classes.Any(x => x.Name == "Class2WithSimpleInterface"));
            Assert.AreEqual(classes.Count(), 2);
        }

        [TestMethod]
        public void CheckOrAllClassesGetByAbstractClass()
        {
            IEnumerable<Type> classes =
                _assemblyHelper.GetAllClassesFromAbstractClass<AbstractClass>("MaxBox.Test.AssemblyDummy");
            Assert.IsTrue(classes.Any(x => x.Name == "Class1FromAbstractClass"));
            Assert.IsTrue(classes.Any(x => x.Name == "Class2FromAbstractClass"));
            Assert.AreEqual(classes.Count(), 2);
        }

        [TestMethod]
        [ExpectedException(typeof (FileNotFoundException))]
        public void CheckOrAssemblyStringIsUsed()
        {
            IEnumerable<Type> classes =
                _assemblyHelper.GetAllClassesFromAbstractClass<AbstractClass>("MaxBox.Test.AssemblyDummy");
            Assert.IsTrue(classes.Count() > 0);
            _assemblyHelper.GetAllClassesFromAbstractClass<AbstractClass>("FalseNoRealAssembly");
        }
    }
}