// Dxflib.Tests
// HatchTests.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-08-26-5:52 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.Entities;
using Dxflib.Entities.Hatch;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class HatchTests
    {
        private const string PathToFile = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\HatchTests.dxf";

        [TestMethod]
        public void PatternNameTest_Get()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            Assert.IsTrue(hatches[0].PatternName == "SOLID");
            Assert.IsTrue(hatches[1].PatternName == "ANSI31");
        }

        [TestMethod]
        public void IsSolidTest()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            Assert.IsTrue(hatches[0].IsSolid);
            Assert.IsFalse(hatches[1].IsSolid);
        }

        [TestMethod]
        public void IsAssociativeTest()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            Assert.IsTrue(!hatches[0].IsAssociative);
            Assert.IsTrue(hatches[1].IsAssociative);
        }

        [TestMethod]
        public void BoundaryLoopsCountTest()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            Assert.IsTrue(hatches[0].BoundaryLoopsCount == 1);
            Assert.IsTrue(hatches[1].BoundaryLoopsCount == 1);
        }

        [TestMethod]
        public void PatternTypeTest()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            Assert.IsTrue(hatches[0].PatternType == HatchPatternType.Predefined);
        }

        [TestMethod]
        public void PatternAngleTest()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            Assert.IsTrue(Math.Abs(hatches[1].PatternAngle - 194) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void PatternScaleTest()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            Assert.IsTrue(Math.Abs(hatches[1].PatternScale - 1) < GeoMath.Tolerance);
        }
    }
}