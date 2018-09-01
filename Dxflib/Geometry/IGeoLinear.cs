using System;
using System.Collections.Generic;
using System.Text;
using Dxflib.LinAlg;

namespace Dxflib.Geometry
{
    /// <summary>
    /// Interface for a Geometric Linear Object
    /// </summary>
    public interface IGeoLinear
    {
        /// <summary>
        /// The Length of the Segment
        /// </summary>
        double Length { get; }

        /// <summary>
        /// The Area of the segment
        /// </summary>
        double Area { get; }

        /// <summary>
        /// Convert the Segment to a Vector
        /// </summary>
        /// <returns></returns>
        Vector ToVector();
    }
}
