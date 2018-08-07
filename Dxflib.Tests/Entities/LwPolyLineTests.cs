// Dxflib.Tests
// LwPolyLineTests.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-07-8:47 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Diagnostics;
using Dxflib.Entities;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class LwPolyLineTests
    {
        private const string PathToFile
            = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LwPolyLineTests.dxf";

        [TestMethod]
        public void NumberOfVertices_Shouldbe5()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polyLines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Assert.IsTrue(polyLines[0].NumberOfVerticies == 5);
        }

        [TestMethod]
        public void PolyLineFlag_ShouldBeClosed()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Assert.IsTrue(polylines[0].PolyLineFlag);
        }

        [TestMethod]
        public void ConstantWidthTest_ShouldBe025()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Assert.IsTrue(Math.Abs(polylines[0].ConstantWidth - 0.25) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void ElevationTests_Shouldbe100()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Assert.IsTrue(Math.Abs(polylines[0].Elevation - 100) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void ThicknessTests_ShouldBe1p5()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Assert.IsTrue(Math.Abs(polylines[0].Thickness - 1.5) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void LengthTestNoBulge_ShouldBe13p6524()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Assert.IsTrue(Math.Abs(polylines[0].Length - 13.6524) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void BulgeTest_LengthShouldBe16p3515()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Assert.IsTrue(Math.Abs(polylines[1].Length - 20.2120) < GeoMath.Tolerance,
                $"Length is: {polylines[1].Length}");
        }

        [TestMethod]
        public void AreaTest_WithoutBulge()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Debug.WriteLine($"Total Area of Polyline is: {polylines[0].Area}");
            Assert.IsTrue(Math.Abs(polylines[0].Area - 11.8750) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void AreaTest_WithBulge()
        {
            var dxfFile = new DxfFile(PathToFile);
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            Debug.WriteLine($"Total Area of Polyline is: {polylines[1].Area}");
            Assert.IsTrue(Math.Abs(polylines[1].Area - 27.0785) < GeoMath.Tolerance);
        }
    }
}