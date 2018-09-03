using System;
using Dxflib.Entities;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void PointPropertiesTests()
        {
            var path = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\PointTests.dxf";
            var dxfFile = new DxfFile(path);
            var points = dxfFile.Entities.GetEntitiesByType<Point>();

            Assert.IsTrue(points.Count > 0);

            var point = points[0];

            Assert.IsTrue(point.LayerName == "0");
            Assert.IsTrue(point.EntityType == typeof(Point));
            Assert.IsTrue(Math.Abs(point.X - 1) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(point.Y - 1) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(point.Z - 0) < GeoMath.Tolerance);
        }
    }
}
