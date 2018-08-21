using System;
using System.Diagnostics;
using Dxflib.Entities;
using Dxflib.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class GeneralEntitiesTests
    {
        [TestMethod]
        public void TestingLinesFromEntities_CastingFromEntityToLine_GettingLength()
        {
            // The testfile
            var testFile =
                new DxfFile(@"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LineParseTest.dxf");

            // Here I want to see if the Entity that is stored in the
            // dxf file will be able to be casted back to a Line without loosing any
            // information.
            double sum = 0;
            foreach (var entity in testFile.Entities)
            {
                // Only cast the entity if the entity type is a line
                // to prevent errors
                Line testLine = null;
                if (entity.EntityType == EntityTypes.Line)
                    testLine = (Line)entity;
                if (testLine != null)
                    sum += testLine.Length;
            }
            Debug.WriteLine(sum); // Print out the sum

            // Asset that the total length of the lines is ...
            Assert.IsTrue(Math.Abs(sum - 85591.0668) < GeoMath.Tolerance);
        }
    }
}
