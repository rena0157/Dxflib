using System;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Geometry
{
    [TestClass]
    public class VertexTests
    {
        [TestMethod]
        public void EqualsTest_VertexAtSamePoint()
        {
            var v0 = new Vertex(2, 4);
            var v1 = new Vertex(2, 4);

            Assert.IsTrue(v0.Equals(v1));
        }

        [TestMethod]
        public void EqualsTest_VertexNotAtSamePoint()
        {
            var v0 = new Vertex(2, 4);
            var v1 = new Vertex(1, 4);

            Assert.IsFalse(v0.Equals(v1));
        }
    }
}
