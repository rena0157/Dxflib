using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dxflib;
using Dxflib.AcadEntities;
using Dxflib.Entities;
using Dxflib.Geometry;
using Dxflib.Parser;

namespace Dxflib.Tests
{
    [TestClass]
    public class DxfFileTests
    {
        [TestMethod]
        public void PrintFileContents()
        {
            // Open the file
            var testFile = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");

            var contents = testFile.DxfFileData;

            for ( int i = 0; i < contents.Length; ++i )
            {
                var currentPair = contents.GetPair(i);
                Debug.WriteLine($"Group Code: {currentPair.GroupCode}, Value: {currentPair.Value}");
            }

        }

        [TestMethod]
        public void PathToFile_Testing()
        {
            // open the file
            var testFile = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");

            // Printout the filename
            Debug.WriteLine(testFile.FileName);

            // Assert that the filename is PrintFileContents
            Assert.IsTrue(testFile.FileName == "PrintFileContents.dxf");
        }

        [TestMethod]
        public void LayerDictionaryTest_GetAllLayers()
        {
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LayerTests.dxf");

            var test = testFile.Layers.GetLayer("TestLayer0").GetAllEntities();

            Assert.IsTrue(test[0].EntityType == EntityTypes.Line);
        }

        [TestMethod]
        public void AutoCadFileVersionTests_TestingTheAutoCadFileVersion()
        {
            var testFile = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");

            Assert.IsTrue(testFile.AutoCADVersion == AutoCadVersions.AC1027);
        }

        [TestMethod]
        public void LastSavedbyTest()
        {
            var testFile = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");

            Assert.IsTrue(testFile.LastSavedBy == "adamf");
        }
    }
}
