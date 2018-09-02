// Dxflib.Tests
// ParserTests.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-04-5:15 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose:

using System.Diagnostics;
using System.Linq;
using Dxflib.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void CurrentLayer_CurrentLayerShouldBe0()
        {
            // The test file
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");
            // Print out the current layer name
            Debug.WriteLine("Current Layer Name:");
            Debug.WriteLine(testFile.CurrentLayer.Name);

            // Assert that the current layer name is "0"
            Assert.IsTrue(testFile.CurrentLayer.Name == "0");
        }

        [TestMethod]
        public void EntityCountTest_ThereShouldBe2EntitesThatAreLines()
        {
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineParseTest.dxf");

            Assert.IsTrue(testFile.Entities.Count == 2);
            Assert.IsTrue(((Line) testFile.Entities.ElementAt(0).Value).EntityType == typeof(Line));
        }

        [TestMethod]
        public void EntityTypeTest_ShouldBe2Lines()
        {
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineParseTest.dxf");
            var linesSum = testFile.Entities.Values.Count(entity => entity.EntityType == typeof(Line));
            Assert.IsTrue(linesSum == 2);
        }
    }
}