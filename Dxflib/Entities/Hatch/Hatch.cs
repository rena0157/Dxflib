// Dxflib
// Hatch.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-08-26-5:13 PM
// By: Adam Renaud
// 
// ============================================================

namespace Dxflib.Entities.Hatch
{
    /// <inheritdoc />
    /// <summary>
    ///     The Hatch Entity
    /// </summary>
    public class Hatch : Entity
    {
        /// <summary>
        /// </summary>
        /// <param name="hb"></param>
        public Hatch(HatchBuffer hb)
        {
            // Base Entity
            LayerName = hb.LayerName;
            Handle = hb.Handle;

            // Hatch Entity
            PatternName = hb.HatchPatternName;
            IsSolid = hb.SolidFillFlag;
            IsAssociative = hb.AssociativityFlag;
            BoundaryLoopsCount = hb.NumberOfLoops;
            Style = hb.HatchStyle;
            PatternType = hb.PatternType;
            PatternAngle = hb.PatternAngle;
            PatternDefLinesCount = hb.NumberOfPatternDefLines;
            PatternScale = hb.PatternScale;
        }

        /// <summary>
        ///     The Hatch Pattern Name
        /// </summary>
        public string PatternName { get; }

        /// <summary>
        ///     Returns true if the hatch has solid fill
        /// </summary>
        public bool IsSolid { get; }

        /// <summary>
        ///     Returns true if the hatch is associative to a polyline
        ///     or a set of lines
        /// </summary>
        public bool IsAssociative { get; }

        /// <summary>
        ///     The total count of boundary loops
        /// </summary>
        public int BoundaryLoopsCount { get; }

        /// <summary>
        ///     The <see cref="HatchStyles" /> style.
        /// </summary>
        public HatchStyles Style { get; }

        /// <summary>
        ///     The <see cref="HatchPatternType" />, pattern type
        /// </summary>
        public HatchPatternType PatternType { get; }

        /// <summary>
        ///     The Pattern Angle.
        /// </summary>
        public double PatternAngle { get; }

        /// <summary>
        /// The number of definition lines in the pattern
        /// </summary>
        public int PatternDefLinesCount { get; }

        /// <summary>
        /// The Pattern Scale
        /// </summary>
        public double PatternScale { get; }
    }
}