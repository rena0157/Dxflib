// Dxflib
// Line.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-05-8:41 AM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;
using Dxflib.Parser;

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
            EntityType = EntityTypes.Line;
            Handle = lineBuffer.handle;
            _layerName = lineBuffer.LayerName;
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
        /// The Thickness of the line
        /// </summary>
        public double Thickness { get; }
    }

    public class LineBuffer : EntityBuffer
    {
        public LineBuffer()
        {
            Thickness = 0;
            X0 = 0;
            X1 = 0;
            Y0 = 0;
            Y1 = 0;
        }

        public double Thickness { get; set; }
        public double X0 { get; set; }
        public double X1 { get; set; }
        public double Y0 { get; set; }
        public double Y1 { get; set; }

        /// <summary>
        /// Main Parsing function for the LineBuffer.
        /// </summary>
        /// <param name="args">Args that are passed by the line
        /// changed event</param>
        /// <returns>True if parse was sucessful</returns>
        public override bool Parse(LineChangeHandlerArgs args)
        {
            // The Current line
            var currentLine = args.NewCurrentLine;

            // See if the base can parse the line
            if (base.Parse(args))
                return true;

            // Switch on the current line to see 
            // if it matches anything that is in the DXF spec.
            switch (currentLine)
            {
                case LineGroupCodes.Thickness:
                    Thickness = double.Parse(args.NewNextLine);
                    return true;
                case LineGroupCodes.X0:
                    X0 = double.Parse(args.NewNextLine);
                    return true;
                case LineGroupCodes.X1:
                    X1 = double.Parse(args.NewNextLine);
                    return true;
                case LineGroupCodes.Y0:
                    Y0 = double.Parse(args.NewNextLine);
                    return true;
                case LineGroupCodes.Y1:
                    Y1 = double.Parse(args.NewNextLine);
                    return true;
                default: return false;
            }
        }
    }

    public static class LineGroupCodes
    {
        public const string Thickness = "39";
        public const string X0 = " 10";
        public const string X1 = " 11";
        public const string Y0 = " 20";
        public const string Y1 = " 21";
    }
}