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

using Dxflib.IO;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     The LineBuffer Class that is used in extraction of the Line Class
    /// </summary>
    public class LineBuffer : EntityBuffer
    {
        /// <summary>
        /// Constructor that will return all values to their defaults
        /// </summary>
        public LineBuffer()
        {
            Thickness = 0;
            X0 = 0;
            X1 = 0;
            Y0 = 0;
            Y1 = 0;
        }

        /// <summary>
        /// The Thickness of the <see cref="Line.Thickness"/>
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>
        /// The X starting coordinate of the line
        /// </summary>
        public double X0 { get; set; }

        /// <summary>
        /// The X ending coordinate of the line
        /// </summary>
        public double X1 { get; set; }

        /// <summary>
        /// The Y starting coordinate of the line
        /// </summary>
        public double Y0 { get; set; }

        /// <summary>
        /// The Y ending coordinate of the line
        /// </summary>
        public double Y1 { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Main Parsing function for the LineBuffer.
        /// </summary>
        /// <remarks>
        ///     This parsing function will first try and parse
        ///     the information for the base class <see cref="T:Dxflib.Entities.EntityBuffer" />.
        ///     If that function returns false then it will attempt to parse based on
        ///     the <see cref="T:Dxflib.Entities.LineGroupCodes" />. If that fails then the line buffer
        ///     does not fill anything and the extraction process moves to the next line.
        /// </remarks>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>True if parse was successful</returns>
        public override bool Parse(TaggedDataList list, int index)
        {
            EntityType = EntityTypes.Line;
            for (var currentIndex = index + 1; 
                currentIndex < list.Length; ++currentIndex)
            {
                var currentData = list.GetPair(currentIndex);

                if (currentData.GroupCode == GroupCodesBase.EntityType)
                    break;

                if (base.Parse(list, currentIndex))
                    continue;

                switch ( currentData.GroupCode )
                {
                    case GroupCodesBase.XPoint:
                        X0 = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.XPointEnd:
                        X1 = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.YPoint:
                        Y0 = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.YPointEnd:
                        Y1 = double.Parse(currentData.Value);
                        continue;
                    case LineGroupCodes.Thickness:
                        Thickness = double.Parse(currentData.Value);
                        continue;
                    default:
                        continue;
                }
            }

            return true;
        }
    }
}