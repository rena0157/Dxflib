// Dxflib.Tests
// GeoArcTests.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-10-7:05 PM
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

        // VVB Tests ---------------------------------------

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

        [TestMethod]
        public void Angle_VertexVertexBulge()
        {
            var vertex0 = new Vertex(0, 2);
            var vertex1 = new Vertex(2, 2);
            const double bulgeValue = -0.5;
            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);
            Assert.IsTrue(Math.Abs(testArc.Angle - 1.85459) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void StartAngleTest_VertexVertexBulge()
        {
            var vertex0 = new Vertex(0, 2);
            var vertex1 = new Vertex(2, 2);
            const double bulgeValue = -0.5;
            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);
            Assert.IsTrue(Math.Abs(
                              testArc.StartAngle - GeoMath.DegToRad(143.1301)) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void EndAngleTest_VertexVertexBulge()
        {
            var vertex0 = new Vertex(0, 2);
            var vertex1 = new Vertex(2, 2);
            const double bulgeValue = -0.5;
            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);
            Assert.IsTrue(Math.Abs(
                              testArc.EndAngle - GeoMath.DegToRad(36.8699)) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void Radius_VertexVertexBulge()
        {
            var vertex0 = new Vertex(0, 2);
            var vertex1 = new Vertex(2, 2);
            const double bulgeValue = -0.5;
            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);
            Assert.IsTrue(Math.Abs(testArc.Radius - 1.25) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void Area_VertexVertexBulge()
        {
            var vertex0 = new Vertex(0, 2);
            var vertex1 = new Vertex(2, 2);
            const double bulgeValue = -0.5;
            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);
            Assert.IsTrue(Math.Abs(testArc.Area - 0.6989) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void Angle_VertexVertexBulge_Changing()
        {
            var vertex0 = new Vertex(0, 2);
            var vertex1 = new Vertex(2, 2);
            const double bulgeValue = -0.5;
            var testArc = new GeoArc(vertex0, vertex1, bulgeValue);

            testArc.Angle = GeoMath.DegToRad(130);
            Assert.IsTrue(Math.Abs(testArc.Angle - GeoMath.DegToRad(130)) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Area - 1.1741) < GeoMath.Tolerance);
        }

        // CAAR Tests -------------------------------------------------------

        [TestMethod]
        public void Vertex0AndVertex1_CaarTest()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            var radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            // Starting Vertex
            Assert.IsTrue(Math.Abs(testArc.Vertex0.X - 2) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex0.Y - 2) < GeoMath.Tolerance);

            // Ending Vertex
            Assert.IsTrue(Math.Abs(testArc.Vertex1.X) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex1.Y - 2) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void BulgeValue_CaarTest()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            var radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            Assert.IsTrue(Math.Abs(testArc.BulgeValue - ( 0.5 )) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void Length_CaarTest()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            var radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            Assert.IsTrue(Math.Abs(testArc.Length - 2.3182 ) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void Area_CaarTest()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            const double radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            Assert.IsTrue(Math.Abs(testArc.Area - 0.6989 ) < GeoMath.Tolerance);
        }

        // PropertyChangingTests ---------------------------
        // CAAR
        [TestMethod]
        public void StartAngle_CaarTest_Changing()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            var radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            testArc.StartAngle = GeoMath.DegToRad(50);
            Assert.IsTrue(Math.Abs(testArc.Vertex0.X - 1.8035) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex0.Y - 2.2076) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void EndAngle_CaarTest_Changing()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            var radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            testArc.EndAngle = GeoMath.DegToRad(200);
            Assert.IsTrue(Math.Abs(testArc.Vertex1.X - -0.1746) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex1.Y - 0.8225) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void Angle_CaarTest_Changing()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            var radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            testArc.Angle = GeoMath.DegToRad(138.1301);
            Assert.IsTrue(Math.Abs(testArc.Vertex1.X - -0.2452) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex1.Y - 1.3589) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void Radius_CaarTest_Changing()
        {
            var centerVertex = new Vertex(1.0, 1.25);
            var startAngle = GeoMath.DegToRad(36.8699);
            var endAngle = GeoMath.DegToRad(143.1301);
            var radius = 1.25;
            var testArc = new GeoArc(centerVertex, startAngle, endAngle, radius);

            testArc.Radius = 2;
            Assert.IsTrue(Math.Abs(testArc.Vertex0.X - 2.6000) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex0.Y - 2.4500) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex1.X - -0.6000) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testArc.Vertex1.Y - 2.4500) < GeoMath.Tolerance);
        }
    }
}