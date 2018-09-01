// Dxflib
// Line.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;
using Dxflib.LinAlg;

namespace Dxflib.Entities
{
    /// <inheritdoc cref="Entity" />
    /// <inheritdoc cref="IGeoLinear" />
    /// <summary>
    ///     The Line Entity Class
    /// </summary>
    public class Line : Entity, IGeoLinear
    {
        /// <inheritdoc />
        /// <summary>
        ///     Extraction Constructor, requires filling out of an <see cref="T:Dxflib.Entities.EntityBuffer" />
        /// </summary>
        /// <param name="lineBuffer">
        ///     The Line Buffer that was filled
        ///     in the extraction process
        /// </param>
        public Line(LineBuffer lineBuffer) : base(lineBuffer)
        {
            Thickness = lineBuffer.Thickness;
            // Setting the GeoLine
            GLine = new GeoLine(new Vertex(lineBuffer.X0, lineBuffer.Y0),
                new Vertex(lineBuffer.X1, lineBuffer.Y1));
        }

        /// <summary>
        ///     The <see cref="GeoLine" /> of the Line, which
        ///     is a sort of geometric backing type for the line
        /// </summary>
        public GeoLine GLine { get; }

        /// <inheritdoc />
        /// <summary>
        ///     The Length of the Line
        /// </summary>
        public double Length => GLine.Length;

        /// <inheritdoc />
        /// <summary>
        /// The Area of the Line
        /// </summary>
        public double Area => GLine.Area;

        /// <inheritdoc />
        /// <summary>
        /// Convert this Line to a Vector
        /// </summary>
        /// <returns></returns>
        public Vector ToVector() => GLine.ToVector();

        /// <summary>
        ///     The Thickness of the line
        /// </summary>
        public double Thickness { get; }
    }
}