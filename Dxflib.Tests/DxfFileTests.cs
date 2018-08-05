using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dxflib;
using Dxflib.AcadEntities;
using Dxflib.Entities;
using Dxflib.Geometry;

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

            // Print the file to screen
            foreach (var line in testFile.ContentStrings)
            {
                Debug.WriteLine(line);
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
        public void Layers_AddingOneLayer_LayerDoesNotExist()
        {
            var testDictionary = new LayerDictionary();
            testDictionary.NewLayer("HelloLayer");

            // Assert there is only one layer
            Assert.IsTrue(testDictionary.Count == 1);
        }

        [TestMethod]
        public void Layers_AddingAnotherLayer_LayerAlreadyExists()
        {
            var testDictionary = new LayerDictionary();
            testDictionary.NewLayer("HelloLayer");
            try
            {
                testDictionary.NewLayer("HelloLayer");
            }
            catch (LayerDictionaryException e)
            {
                Debug.WriteLine(e.Message);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestingLinesFromEntities_CastingFromEntityToLine_GettingLength()
        {
            // The testfile
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineParseTest.dxf");

            // Here I want to see if the Entity that is stored in the
            // dxf file will be able to be casted back to a Line without loosing any
            // information.
            double sum = 0;
            foreach (var entity in testFile.Entities)
            {
                // Only cast the entity if the entity type is a line
                // to prevent errors
                Line testLine = null;
                if (entity.EntityType == EntityTypes.Line)
                    testLine = (Line)entity;
                if (testLine != null)
                    sum += testLine.Length;
            }
            Debug.WriteLine(sum); // Print out the sum

            // Asset that the total length of the lines is ...
            Assert.IsTrue(Math.Abs(sum - 85591.0668) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void LayerDictionaryTest_GetAllLayers()
        {
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LayerTests.dxf");

            var test = testFile.Layers.GetLayer("TestLayer0").GetAllEntities();

            Assert.IsTrue(test[0].EntityType == EntityTypes.Line);
        }
    }
}
