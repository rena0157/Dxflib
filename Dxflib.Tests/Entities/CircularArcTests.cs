using System;
using Dxflib.Entities;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class CircularArcTests
    {
        [TestMethod]
        public void PropertiesTests()
        {
            var pathToFile = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\CircularArcTests.dxf";
            var dxfFile = new DxfFile(pathToFile);

            var arcs = dxfFile.Entities.GetEntitiesByType<CircularArc>();

            // The Number of Arcs should be 1
            Assert.IsTrue(arcs.Count == 1);

            // Get the first Arc
            var arc = arcs[0];

            // Testing Properties
            Assert.IsTrue(Math.Abs(arc.Thickness - 1.0) < GeoMath.Tolerance);
            Assert.IsTrue(arc.CenterPoint.Equals(new Vertex(5.9532, -2.5770)));
            Assert.IsTrue(arc.MiddleVertex.Equals(new Vertex(3.5163, 2.6345)));
            Assert.AreEqual(arc.StartingVertex, new Vertex(6.2537, 3.1682));
            Assert.AreEqual(arc.EndingVertex, new Vertex(1.3516, 0.8760));
            Assert.IsTrue(Math.Abs(arc.StartAngle - GeoMath.DegToRad(87.0059)) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(arc.EndAngle - GeoMath.DegToRad(143.1154)) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(arc.Radius - 5.7531) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(arc.Length - 5.6340) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(arc.Area - 2.4690) < GeoMath.Tolerance);
        }
    }
}
