// Dxflib.Tests
// UnitTest1.cs
// 
// ============================================================
// 
// Created: 2018-09-03
// Last Updated: 2018-09-03-12:32 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.Entities;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        public void CirclePropertiesTests()
        {
            const string path = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\CircleTests.dxf";
            var dxfFile = new DxfFile(path);

            var circles = dxfFile.Entities.GetEntitiesByType<Circle>();

            Assert.IsTrue(circles.Count > 0);

            var circle = circles[0];

            Assert.IsTrue(circle.CenterPoint.Equals(new Vertex(2.5, 2.5)));
            Assert.IsTrue(Math.Abs(circle.Radius - 2) < GeoMath.Tolerance);
        }
    }
}