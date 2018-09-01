using System;
using System.ComponentModel;
using System.Diagnostics;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class VertexTests
    {
        private static bool _eventWorked;
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
            testVertex.PropertyChanged += TestVertexOnPropertyChanged;
            testVertex.X = 2;
            Assert.IsTrue(_eventWorked);
        }

        private static void TestVertexOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _eventWorked = true;
            Debug.WriteLine(e.PropertyName);
        }
    }
}
