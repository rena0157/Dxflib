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

using Dxflib.Parser;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     The LineBuffer Class that is used in extraction of the Line Class
    /// </summary>
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
        ///     Main Parsing function for the LineBuffer.
        /// </summary>
        /// <param name="args">
        ///     Args that are passed by the line
        ///     changed event
        /// </param>
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
}