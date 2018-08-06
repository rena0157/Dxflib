using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dxflib.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.Entities
{
    [TestClass]
    public class LwPolyLineTests
    {
        private const string PathToFile
            = @"C:\Dev\Dxflib\Dxflib.Tests\DxfTestFiles\LwPolyLineTests.dxf";
        
        [TestMethod]
        public void NumberOfVertices_ShouldMatchCountOfXandY()
        {
            var dxfFile = new DxfFile(PathToFile);

            var polyLines = new List<LwPolyLine>();

            foreach (var entity in dxfFile.Entities)
            {
                if (entity.EntityType == EntityTypes.Lwpolyline)
                    polyLines.Add((LwPolyLine)entity);
            }
            Debug.WriteLine(polyLines.Count);
        }
    }
}
