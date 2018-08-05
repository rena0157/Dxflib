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
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineParseTest.dxf");

            double sum = 0;
            foreach (var entity in testFile.Entities)
            {
                Line testLine = null;
                if (entity.EntityType == EntityTypes.Line)
                    testLine = (Line)entity;
                if (testLine != null)
                    sum += testLine.Length;
            }
            Debug.WriteLine(sum);
            Assert.IsTrue(Math.Abs(sum - 85591.0668) < GeoMath.Tolerance);
        }
    }
}
