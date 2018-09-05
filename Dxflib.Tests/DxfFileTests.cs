// Dxflib.Tests
// DxfFileTests.cs
// 
// ============================================================
// 
// Created: 2018-08-03
// Last Updated: 2018-08-21-8:54 PM
// By: Adam Renaud
// 
// ============================================================

using System.Diagnostics;
using Dxflib.Entities;
using Dxflib.IO;
using Dxflib.IO.Header;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            for ( var i = 0; i < contents.Length; ++i )
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

            Assert.IsTrue(test[0].EntityType == typeof(Line));
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

        [TestMethod]
        public void LastWriteTimeTest()
        {
            var testFile = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PrintFileContents.dxf");

            Debug.WriteLine(testFile.LastWriteTime);
        }
    }
}