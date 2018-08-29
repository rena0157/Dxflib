using System;
using Dxflib.AcadEntities;
using Dxflib.AcadEntities.Pointer;
using Dxflib.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.AcadEntitiesTests
{
    [TestClass]
    public class EntityPointerTests
    {
        [TestMethod]
        public void GetReferenceEntityAsLine()
        {
            var buffer = new LineBuffer()
            {
                Handle = "2f8",
                LayerName = "TestLayer",
                EntityType = EntityTypes.Line,
                Thickness = 0.0,
                X0 = 0.0,
                X1 = 3.0,
                Y0 = 0.0,
                Y1 = 4.0
            };
            var line = new Line(buffer);

            var testPointer = new LinePointer(line.Handle) {RefEntity = line};
            Assert.IsTrue(testPointer.EntityType == typeof(Line));
            Assert.IsTrue(testPointer.RefEntity == line);
        }
    }
}
