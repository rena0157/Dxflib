// Dxflib.Tests
// GeoLineTests.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-05-7:59 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class GeoLineTests
    {
        [TestMethod]
        public void ChangeVertexProperty_UpdateLengthCheck()
        {
            // Set up
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testLine = new GeoLine(vertex0, vertex1);

            // The length should be 5
            Assert.IsTrue(Math.Abs(testLine.Length - 5) < GeoMath.Tolerance);

            // Changing the vertex property should recalcuate the length
            testLine.Vertex0.X = 3;
            testLine.Vertex1.Y = 3;


            // The length should now be 4
            Assert.IsTrue(Math.Abs(testLine.Length - 3) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void ChangeingVertexReference_ShouldUpdateGeometry()
        {
            // Set up
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testLine = new GeoLine(vertex0, vertex1);

            // Length should be 5
            Assert.IsTrue(Math.Abs(testLine.Length - 5) < GeoMath.Tolerance);

            // Change the vertex to see if lenght updates
            testLine.Vertex0 = new Vertex(3, 0);

            // Lenght should now be 4
            Assert.IsTrue(Math.Abs(testLine.Length - 4) < GeoMath.Tolerance);
        }
    }
}