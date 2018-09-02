// Dxflib
// Hatch.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.AcadEntities.Pointer;
using Dxflib.Geometry;

namespace Dxflib.Entities.Hatch
{
    /// <inheritdoc />
    /// <summary>
    ///     The Hatch Entity
    /// </summary>
    public class Hatch : Entity
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="hb"></param>
        public Hatch(HatchBuffer hb) : base(hb)
        {
            EntityType = hb.EntityType;

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

            // Boundary
            if ( !IsAssociative )
                Boundary = hb.Boundary;
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
        ///     The number of definition lines in the pattern
        /// </summary>
        public int PatternDefLinesCount { get; }

        /// <summary>
        ///     The Pattern Scale
        /// </summary>
        public double PatternScale { get; }

        /// <summary>
        ///     The Boundary Path as a polyline
        /// </summary>
        public GeoPolyline Boundary { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     This function will build the <see cref="P:Dxflib.Entities.Hatch.Hatch.Boundary" />
        ///     of this hatch if it is <see cref="P:Dxflib.Entities.Hatch.Hatch.IsAssociative" />.
        /// </summary>
        public override void UpdateReferencedEntities()
        {
            if ( ReferencedEntities.Count > 1 )
                Boundary = BuildBoundary();
            else if ( ReferencedEntities.Count == 1 )
                if ( ReferencedEntities[0].RefEntity is LwPolyLine polyline )
                    Boundary = polyline.GPolyline;
        }

        // Build the Boundary if the number
        // of boundary objects is greater than 1
        // or in other words is not a polyline
        private GeoPolyline BuildBoundary()
        {
            var geoPolyline = new GeoPolyline();
            foreach ( var entityPointer in ReferencedEntities )
                switch ( entityPointer.RefEntity )
                {
                    case Line line:
                        geoPolyline.Add(line.GLine);
                        continue;
                    case CircularArc arc:
                        geoPolyline.Add(arc.GeometricArc);
                        continue;
                    default:
                        throw new EntityPointerException("Pointer Type Not Recognized");
                }
            return geoPolyline;
        }
    }
}