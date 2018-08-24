// Dxflib
// Line.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-07-11:05 AM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     The Line Entity Class
    /// </summary>
    public class Line : Entity
    {
        /// <summary>
        ///     Extraction Constructor, requires filling out of an <see cref="EntityBuffer"/>
        /// </summary>
        /// <param name="lineBuffer">
        ///     The Line Buffer that was filled
        ///     in the extraction process
        /// </param>
        public Line(LineBuffer lineBuffer)
        {
            // Setting up variables from the lineBuffer
            EntityType = EntityTypes.Line;
            Handle = lineBuffer.Handle;
            LayerNameBf = lineBuffer.LayerName;
            Thickness = lineBuffer.Thickness;

            // Setting the GeoLine
            GLine = new GeoLine(new Vertex(lineBuffer.X0, lineBuffer.Y0),
                new Vertex(lineBuffer.X1, lineBuffer.Y1));
        }

        /// <summary>
        ///     The <see cref="GeoLine"/> of the Line, which
        ///     is a sort of geometric backing type for the line
        /// </summary>
        public GeoLine GLine { get; }

        /// <summary>
        ///     The Length of the Line
        /// </summary>
        public double Length => GLine.Length;

        /// <summary>
        ///     The Thickness of the line
        /// </summary>
        public double Thickness { get; }
    }
}