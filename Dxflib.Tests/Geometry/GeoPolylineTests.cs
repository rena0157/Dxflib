using System;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Geometry
{
    [TestClass]
    public class GeoPolylineTests
    {
        // This file contains all of the information
        // regarding the geometry used below
        // @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\GeoPolylineTests.dxf";
        [TestMethod]
        public void GenericGeoPolylineTest_1Arc()
        {
            // Line 0
            var v0 = new Vertex(3.5, 0);
            var v1 = new Vertex(6, 0);
            var l0 = new GeoLine(v0, v1);
            Assert.IsTrue(Math.Abs(l0.Length - 2.5) < GeoMath.Tolerance);

            // Line 1
            var v2 = new Vertex(6, 2.5);
            var l1 = new GeoLine(v1, v2);
            Assert.IsTrue(Math.Abs(l1.Length - 2.5) < GeoMath.Tolerance);

            // Arc
            var centerPoint = new Vertex(4.75, 1.75);
            var startAngle = GeoMath.DegToRad(30.964);
            var endAngle = GeoMath.DegToRad(149.036);
            const double radius = 1.4577;
            var arc0 = new GeoArc(centerPoint, startAngle, endAngle, radius);
            Assert.IsTrue(Math.Abs(arc0.Length - 3.0040) < GeoMath.Tolerance);

            // Line 2
            var v3 = new Vertex(3.5, 2.5);
            var l2 = new GeoLine(v3, v0);
            Assert.IsTrue(Math.Abs(l2.Length - 2.5) < GeoMath.Tolerance);

            // GeoPolyline
            var geoPolyline = new GeoPolyline(); // Initialized

            // Adding sections to the GeoPolyline
            geoPolyline.Add(l0);
            geoPolyline.Add(l1);
            geoPolyline.Add(arc0);
            geoPolyline.Add(l2);

            // Assert
            Assert.IsTrue(Math.Abs(geoPolyline.Length - 10.5040) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(geoPolyline.Area - 7.5021) < GeoMath.Tolerance);
        }
    }
}
