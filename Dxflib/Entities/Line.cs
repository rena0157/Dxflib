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
        ///     Main Constructor
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
        ///     The GeoLine of the Line
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

    /// <summary>
    ///     The Group codes for the Line Entity
    /// </summary>
    public static class LineGroupCodes
    {
        public const string Thickness = "39";
        public const string X0 = " 10";
        public const string X1 = " 11";
        public const string Y0 = " 20";
        public const string Y1 = " 21";
    }
}