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
        public void HatchBoundary_PolylineBoundary_NoArcs()
        {
            // Open File
            var file = new DxfFile(PathToFile);

            // Get hatches - Note that Hatch is associative
            var hatches = file.Entities.GetEntitiesByType<Hatch>();
            var hatch = hatches[0];

            // Test the boundary
            Assert.IsTrue(Math.Abs(hatch.Boundary.Length - 10) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(hatch.Boundary.Area - 6.2500) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void HatchBoundary_Polyline_Arcs()
        {
            // Open File
            var file = new DxfFile(PathToFile);
            // Get Hatches - Note that Hatch is associative
            var hatches = file.Entities.GetEntitiesByType<Hatch>();

            // This Hatch was drawn clockwise
            Assert.IsTrue(Math.Abs(hatches[1].Boundary.Length - 10.5040) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(hatches[1].Boundary.Area - 7.5021) < GeoMath.Tolerance);
            
            // This Hatch was drawn Counter clockwise
            Assert.IsTrue(Math.Abs(hatches[2].Boundary.Length - 10.5040) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(hatches[2].Boundary.Area - 7.5021) < GeoMath.Tolerance);
        }

        [TestMethod]
        public void LineWithArcBoundary_NotAssociative()
        {
            // Open File
            var file = new DxfFile(PathToFile);
            // Get Hatches - Note that Hatch is not associative
            var hatches = file.Entities.GetEntitiesByType<Hatch>();

            // Hatch with 4 different segments that are lines and arcs
            var hatch = hatches[3];
            Assert.IsTrue(Math.Abs(hatch.Boundary.Area - 7.5021) < GeoMath.Tolerance);
            Assert.IsTrue(Math.Abs(hatch.Boundary.Length - 10.5040) < GeoMath.Tolerance);
        }
    }
}
