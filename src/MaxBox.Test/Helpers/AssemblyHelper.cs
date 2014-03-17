using System.Linq;
using MaxBox.AssemblyDummy;
using MaxBox.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaxBox.Test.Helpers
{
    [TestClass]
    public class AssemblyHelperUnitTest
    {
        [TestMethod]
        public void CheckOrAllClassesGetLoadedByInterface()
        {
            var assemblyHelper = new AssemblyHelper();
            var classes = assemblyHelper.GetAllClassesWithInterface<ISimpleInterface>("MaxBox.Test.AssemblyDummy");
            Assert.AreEqual(classes.First().Name, "Class1");
            Assert.AreEqual(classes.Count(), 2);

        }
    }
}
