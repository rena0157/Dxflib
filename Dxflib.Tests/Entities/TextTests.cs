// Dxflib.Tests
// TextTests.cs
// 
// ============================================================
// 
// Created: 2018-09-02
// Last Updated: 2018-09-02-5:49 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.Entities.Text;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class TextTests
    {
        [TestMethod]
        public void TextPropertiesTest()
        {
            const string path = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\TextTests.dxf";

            var dxfFile = new DxfFile(path);

            var textList = dxfFile.Entities.GetEntitiesByType<Text>();
            Assert.IsTrue(textList.Count == 1);

            var text = textList[0];

            Assert.IsTrue(!text.IsBackwards);
            Assert.IsTrue(!text.IsUpsideDown);
            Assert.IsTrue(!text.IsAnnotative);
            Assert.IsTrue(text.Contents == "Hello World");
            Assert.IsTrue(text.TextStyle.Name == "STANDARD");
            Assert.IsTrue(text.Justify == JustifyOptions.Left);
            Assert.IsTrue(Math.Abs(text.Height - 0.2) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(text.Rotation) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(text.WidthFactor - 1) < GeoMath.Tolerance);
            Assert.IsTrue(text.PositionVertex.Equals(new Vertex(0.8751, 0.9372)));
        }

        [TestMethod]
        public void MTextPropertiesTest()
        {
            const string path = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\TextTests.dxf";

            var dxfFile = new DxfFile(path);
            var textList = dxfFile.Entities.GetEntitiesByType<MText>();
            Assert.IsTrue(textList.Count == 1);

            var text = textList[0];

            Assert.IsTrue(text.Contents == "Hello MText");
            Assert.IsTrue(text.TextStyle.Name == "STANDARD");
            Assert.IsTrue(text.Justify == JustifyOptions.TopLeft);
            Assert.IsTrue(text.PositionVertex.Equals(new Vertex(3.8778, 1.2520)));
            Assert.IsTrue(Math.Abs(text.ReferenceRectangleWidth - 2.09316) < GeoMath.Tolerance);
            Assert.IsTrue(text.DrawDirection == DrawDirections.ByStyle);
        }
    }
}