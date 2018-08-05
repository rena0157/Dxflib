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
using Dxflib.Entities;
using Dxflib.Parser;
using Dxflib.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Controller_PrintoutTest()
        {
            // var testFile = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");
            Debug.WriteLine("Nothing Should Happen here");
        }

        [TestMethod]
        public void VersionTest_VersionShouldBeAutoCAD2013()
        {
            // Create the test file
            var testFile = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");

            // Print out the version 
            Debug.WriteLine(
                $"AutoCAD Version: {testFile.AutoCADVersion}({DxflibTools.GetEnumDescription(testFile.AutoCADVersion)})");

            // Assert
            Assert.IsTrue(AutoCADVersions.AC1027 == testFile.AutoCADVersion);
        }

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
        public void LastSavedByTest_ShouldBeadamf()
        {
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");
            Debug.WriteLine($"The file was last saved by: {testFile.LastSavedBy}");
            Assert.IsTrue(testFile.LastSavedBy == "adamf");
        }

        [TestMethod]
        public void EntityCountTest_ThereShouldBe2EntitesThatAreLines()
        {
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineParseTest.dxf");

            Assert.IsTrue(testFile.Entities.Count == 2);
            Assert.IsTrue(((Line) testFile.Entities[0]).EntityType == EntityTypes.Line);
        }

        [TestMethod]
        public void EntityTypeTest_Shouldbe2Lines()
        {
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineParseTest.dxf");
            var linesSum = 0;
            foreach (var entity in testFile.Entities)
            {
                if (entity.EntityType == EntityTypes.Line)
                    ++linesSum;
            }
            Assert.IsTrue(linesSum == 2);
        }
    }
}