// Dxflib.Tests
// VectorTests.cs
// 
// ============================================================
// 
// Created: 2018-08-08
// Last Updated: 2018-08-08-6:57 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Geometry
{
    [TestClass]
    public class VectorTests
    {
        /// <summary>
        ///     Tests the basic constructor in the vector class
        /// </summary>
        [TestMethod]
        public void VectorConstructor_BasicConstructor()
        {
            var testVector = new Vector();
            Assert.IsTrue(Math.Abs(testVector.X - 1) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.Y - 1) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.Z - 1) < GeoMath.Tolerance);
        }

        /// <summary>
        ///     Tests the Vertex Constructor in the vertex class
        /// </summary>
        [TestMethod]
        public void VectorConstructor_VertexConstructor()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);
            Assert.IsTrue(Math.Abs(testVector.Length - 5) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.X - 3) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.Y - 4) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.Z - 0) < GeoMath.Tolerance);
        }

        /// <summary>
        ///     Tests the x,y,z Component Constructor in the vector class
        /// </summary>
        [TestMethod]
        public void VectorConstructor_ComponentConstructor()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var initalVector = new Vector(vertex0, vertex1);
            var testVector = new Vector(initalVector.X, initalVector.Y);
            Assert.IsTrue(Math.Abs(testVector.Length - 5) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.X - 3) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.Y - 4) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(testVector.Z - 0) < GeoMath.Tolerance);
            Assert.IsFalse(testVector.HeadVertex == vertex1);
        }

        /// <summary>
        ///     Tests that the Geometry of the vector will update
        ///     if the vertex of a vector is changed externally
        /// </summary>
        [TestMethod]
        public void VectorUpdateGeometryTest_ChangingVertex()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);
            Assert.IsTrue(Math.Abs(testVector.Length - 5) < GeoMath.Tolerance);
            testVector.TailVertex = new Vertex(3, 0);
            Assert.IsTrue(Math.Abs(testVector.Length - 4) < GeoMath.Tolerance);
        }

        /// <summary>
        ///     Tests that the subs to the vertex GeometryChanged Event will
        ///     Cause the geometry of the vector to update
        /// </summary>
        [TestMethod]
        public void VectorUpdateGeometryTest_ChangeVertexProperty()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);
            Assert.IsTrue(Math.Abs(testVector.Length - 5) < GeoMath.Tolerance);
            testVector.TailVertex.X = 3;
            Assert.IsTrue(Math.Abs(testVector.Length - 4) < GeoMath.Tolerance);
        }

        /// <summary>
        ///     Tests the method that convertes the Vector to a unit vector in the direction
        ///     of the vector
        /// </summary>
        [TestMethod]
        public void VectorToUnitVectorTest()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);
            var unitVector = testVector.ToUnitVector();
            Assert.IsTrue(Math.Abs(unitVector.Length - 1) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(unitVector.X - testVector.X / testVector.Length)
                          < GeoMath.Tolerance);
        }

        /// <summary>
        ///     Tests the Dot product functionality of the vector class
        /// </summary>
        [TestMethod]
        public void VectorDotProductTests()
        {
            var vertex00 = new Vertex(0, 0);
            var vertex01 = new Vertex(3, 4);
            var testVector1 = new Vector(vertex00, vertex01);

            var vertex10 = new Vertex(0, 0);
            var vertex11 = new Vertex(1, 1);
            var testVector2 = new Vector(vertex10, vertex11);

            var dotProduct = testVector1.DotProduct(testVector2);
            Assert.IsTrue(Math.Abs(dotProduct - 7) < GeoMath.Tolerance);
        }

        /// <summary>
        ///     Tests the Cross product functionality of the vector class
        /// </summary>
        [TestMethod]
        public void VectorCrossProductTest()
        {
            var vertex00 = new Vertex(0, 0);
            var vertex01 = new Vertex(3, 4);
            var testVector1 = new Vector(vertex00, vertex01);

            var vertex10 = new Vertex(0, 0);
            var vertex11 = new Vertex(1, 0);
            var testVector2 = new Vector(vertex10, vertex11);

            var crossProduct = testVector1.CrossProduct(testVector2);
            Assert.IsTrue(Math.Abs(crossProduct.Length - 4) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void VectorOperator_Addition()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);

            var testVector2 = new Vector(1, 1);

            var additionTest = testVector + testVector2;
            Assert.IsTrue(Math.Abs(additionTest.X - 4) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Y - 5) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Z - 0) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void VectorOperator_Subtraction()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);

            var testVector2 = new Vector(1, 1);

            var additionTest = testVector - testVector2;
            Assert.IsTrue(Math.Abs(additionTest.X - 2) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Y - 3) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Z - 0) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void VectorOperator_ScalerMultiplication()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);

            var additionTest = 2*testVector;
            Assert.IsTrue(Math.Abs(additionTest.X - 6) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Y - 8) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Z - 0) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void VectorOperator_ScalerDivision()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);

            var additionTest = testVector/2;
            Assert.IsTrue(Math.Abs(additionTest.X - 1.5) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Y - 2) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(additionTest.Z - 0) < GeoMath.Tolerance);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void VectorOperator_ScalerDivision_DivideByZero()
        {
            var vertex0 = new Vertex(0, 0);
            var vertex1 = new Vertex(3, 4);
            var testVector = new Vector(vertex0, vertex1);

            var additionTest = testVector/0;
        }
    }
}