using System;
using System.Diagnostics;
using Dxflib.AcadEntities;
using Dxflib.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dxflib.Tools;

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

    }
}
