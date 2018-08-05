using System;
using System.Diagnostics;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class VertexTests
    {
        private static bool _eventworked = false;
        [TestMethod]
        public void TestingDefaultValueForZ()
        {
            var testVertex = new Vertex(1, 2);
            Assert.IsTrue(Math.Abs(testVertex.Z) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void TestingRaisedEventForXYZ_GeometryChanged()
        {
            var testVertex = new Vertex(1, 2, 3);
            testVertex.GeometryChanged += TestVertexOnGeometryChanged;
            testVertex.X = 2;
            Assert.IsTrue(_eventworked);
        }

        private static void TestVertexOnGeometryChanged(object sender, GeometryChangedHandlerArgs args)
        {
            Debug.WriteLine(args.Name);
            _eventworked = true;
        }
    }
}
