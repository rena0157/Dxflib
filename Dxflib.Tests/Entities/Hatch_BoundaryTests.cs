using System;
using Dxflib.Entities;
using Dxflib.Entities.Hatch;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class Hatch_BoundaryTests
    {
        private const string PathToFile 
            = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\Hatch_BoundaryTests.dxf";
        [TestMethod]
        public void LineBoundary()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.GetEntitiesByType<Hatch>(EntityTypes.Hatch);
            var hatch = hatches[0];
            Assert.IsTrue(Math.Abs(hatch.Boundary.Length - 10) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void LineWithArcBoundary()
        {
            var file = new DxfFile(PathToFile);
            var hatches = file.GetEntitiesByType<Hatch>(EntityTypes.Hatch);
            var hatch = hatches[1];
            Assert.IsTrue(Math.Abs(hatch.Boundary.Length - 10.5040) < GeoMath.Tolerance);
        }
    }
}
