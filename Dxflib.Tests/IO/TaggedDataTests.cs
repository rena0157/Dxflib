using Dxflib.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.IO
{
    [TestClass]
    public class TaggedDataTests
    {
        [TestMethod]
        public void ConvertTypeTest_String()
        {
            var testData = new TaggedData("  8", "ThisIsALayer");

            Assert.IsTrue(testData.GetValueType == typeof(string));
        }

        [TestMethod]
        public void ConvertTypeTest_DoubleCast()
        {
            var testData = new TaggedData(" 10", "100.2");

            Assert.IsTrue(testData.GetValueType == typeof(double));
        }
    }
}
