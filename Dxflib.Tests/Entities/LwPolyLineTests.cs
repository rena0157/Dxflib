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

        [TestMethod]
        public void LengthAndAreaTest_0Bulge()
        {
            var dxfFile 
                = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\BulgeAreaAndLengthTests.dxf");
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            // Area
            Assert.IsTrue(Math.Abs(polylines[0].Area - 131.6307) < GeoMath.Tolerance, 
                $"Area: {polylines[0].Area}");
            // Length
            Assert.IsTrue(Math.Abs(polylines[0].Length - 44.1223) < GeoMath.Tolerance,
                $"Length: {polylines[0].Length}");
        }

        [TestMethod]
        public void LengthAndAreaTest_1Bulge()
        {
            var dxfFile 
                = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\BulgeAreaAndLengthTests.dxf");
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            // Area
            Assert.IsTrue(Math.Abs(polylines[1].Area - 126.0766) < GeoMath.Tolerance, 
                $"Area: {polylines[1].Area}");
            // Length
            Assert.IsTrue(Math.Abs(polylines[1].Length - 44.3695) < GeoMath.Tolerance,
                $"Length: {polylines[1].Length}");
        }

        [TestMethod]
        public void LengthAndAreaTest_2Bulge()
        {
            var dxfFile 
                = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\BulgeAreaAndLengthTests.dxf");
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            // Area
            Assert.IsTrue(Math.Abs(polylines[2].Area - 129.7362) < GeoMath.Tolerance, 
                $"Area: {polylines[2].Area}");
            // Length
            Assert.IsTrue(Math.Abs(polylines[2].Length - 44.8019) < GeoMath.Tolerance,
                $"Length: {polylines[2].Length}");
        }

        [TestMethod]
        public void LengthAndAreaTest_3Bulge()
        {
            var dxfFile 
                = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\BulgeAreaAndLengthTests.dxf");
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            // Area
            Assert.IsTrue(Math.Abs(polylines[3].Area - 125.0200) < GeoMath.Tolerance, 
                $"Area: {polylines[3].Area}");
            // Length
            Assert.IsTrue(Math.Abs(polylines[3].Length - 45.1231) < GeoMath.Tolerance,
                $"Length: {polylines[3].Length}");
        }

        [TestMethod]
        public void LengthAndAreaTest_4Bulge()
        {
            var dxfFile 
                = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\BulgeAreaAndLengthTests.dxf");
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            // Area
            Assert.IsTrue(Math.Abs(polylines[4].Area - 128.0246) < GeoMath.Tolerance, 
                $"Area: {polylines[4].Area}");
            // Length
            Assert.IsTrue(Math.Abs(polylines[4].Length - 45.6533) < GeoMath.Tolerance,
                $"Length: {polylines[4].Length}");
        }

        [TestMethod]
        public void LengthAndAreaTest_5Bulge()
        {
            var dxfFile 
                = new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\BulgeAreaAndLengthTests.dxf");
            var polylines = dxfFile.GetEntitiesByType<LwPolyLine>(EntityTypes.Lwpolyline);
            // Area
            Assert.IsTrue(Math.Abs(polylines[5].Area - 121.5797) < GeoMath.Tolerance, 
                $"Area: {polylines[5].Area}");
            // Length
            Assert.IsTrue(Math.Abs(polylines[5].Length - 45.9308) < GeoMath.Tolerance,
                $"Length: {polylines[5].Length}");
        }
    }
}