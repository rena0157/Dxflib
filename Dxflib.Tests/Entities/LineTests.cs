// Dxflib.Tests
// LineTests.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-08-05-8:16 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Linq;
using Dxflib.Entities;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class LineTests
    {
        private const string PathToFile =
            @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineTests.dxf";

        [TestMethod]
        public void EntityTypeTest_ShouldBeLine()
        {
            var testFile = new DxfFile(PathToFile);

            // Cast the element to a line
            var line = (Line) testFile.Entities.ElementAt(0).Value;

            // Assert
            Assert.IsTrue(line.EntityType == typeof(Line));
        }

        [TestMethod]
        public void LayerNameTest_LayerNameShouldBeTestLayer0()
        {
            // The test file
            var testFile = new DxfFile(PathToFile);

            // The test line
            var line = (Line) testFile.Entities.ElementAt(0).Value;

            // Assert
            Assert.IsTrue(line.LayerName == "TestLayer0",
                $"The layer name is: {line.LayerName}");
        }

        [TestMethod]
        public void GeometryChangedTest_LengthShouldUpdate()
        {
            // The file where the line is located
            var testFile = new DxfFile(PathToFile);

            // The line that will be tested
            var testLine = (Line) testFile.Entities.ElementAt(0).Value;

            // Check to make sure that the initial length is 4
            Assert.IsTrue(Math.Abs(testLine.Length - 5) < GeoMath.Tolerance);

            // Change the vertex to see if the length updates
            testLine.GLine.Vertex0 = new Vertex(4, 0);

            Assert.IsTrue(Math.Abs(testLine.Length - 3) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void ThicknessTest_ThicknessShouldBe0()
        {
            var testFile = new DxfFile(PathToFile);
            var testLine = (Line) testFile.Entities.ElementAt(0).Value;
            Assert.IsTrue(Math.Abs(testLine.Thickness) < GeoMath.Tolerance);
        }
    }
}