// Dxflib.Tests
// GeoArcTests.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-07-6:49 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Diagnostics;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Geometry
{
    [TestClass]
    public class GeoArcTests
    {
        [TestMethod]
        public void LengthTest_LengthShouldBe5p05832()
        {
            var vertex0 = new Vertex(13, 3.5);
            var vertex1 = new Vertex(13, 0);
            var bulgeValue = Math.Tan(GeoMath.DegToRad(164) / 4);

            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);

            Debug.WriteLine($"The Total length of the Arc is {testArc.Length}");
            Assert.IsTrue(Math.Abs(testArc.Length - 5.05832) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void CenterVertexTests()
        {
            var vertex1 = new Vertex(2, 2);
            var vertex0 = new Vertex(0, 2);
            const double bulgeValue = -0.5;

            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);

            Assert.IsTrue(Math.Abs(testArc.CenterVertex.X - 1) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.CenterVertex.Y - 1.25) < GeoMath.Tolerance);
        }
    }
}