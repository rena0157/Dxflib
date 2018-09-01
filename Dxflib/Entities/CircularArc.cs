// Dxflib
// CircularArc.cs
// 
// ============================================================
// 
// Created: 2018-09-01
// Last Updated: 2018-09-01-9:39 AM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// The Circular Arc Entity
    /// </summary>
    public class CircularArc : Entity
    {
        // The GeoArc Backing Field

        /// <inheritdoc />
        /// <summary>
        ///     The <see cref="T:Dxflib.Entities.CircularArcBuffer" /> constructor
        ///     for the Arc Entity
        /// </summary>
        /// <param name="ab">The <see cref="T:Dxflib.Entities.CircularArcBuffer" /></param>
        public CircularArc(CircularArcBuffer ab) : base(ab)
        {
            EntityType = EntityTypes.CircularArc;
            GeometricArc = new GeoArc(
                new Vertex(ab.CenterPointX, ab.CenterPointY, ab.CenterPointZ),
                GeoMath.DegToRad(ab.StartAngle),
                GeoMath.DegToRad(ab.EndAngle),
                ab.Radius
            );
            Thickness = ab.Thickness;
        }

        /// <summary>
        /// The Thickness of the Arc
        /// </summary>
        public double Thickness { get; }

        /// <summary>
        ///     The Center Vertex of the Arc
        /// </summary>
        public Vertex CenterPoint => GeometricArc.CenterVertex;

        /// <summary>
        ///     The Middle Vertex of the Arc
        /// </summary>
        public Vertex MiddleVertex => GeometricArc.MiddleVertex;

        /// <summary>
        /// The Starting Vertex
        /// </summary>
        public Vertex StartingVertex => GeometricArc.Vertex0;

        /// <summary>
        /// The Ending Vertex
        /// </summary>
        public Vertex EndingVertex => GeometricArc.Vertex1;

        /// <summary>
        /// The Starting Angle of the Arc
        /// </summary>
        public double StartAngle => GeometricArc.StartAngle;

        /// <summary>
        /// The End Angle of the Arc
        /// </summary>
        public double EndAngle => GeometricArc.EndAngle;

        /// <summary>
        /// The Radius of the Arc
        /// </summary>
        public double Radius => GeometricArc.Radius;

        /// <summary>
        /// The Arc Length of the Arc
        /// </summary>
        public double Length => GeometricArc.Length;

        /// <summary>
        /// The Area of the Arc
        /// </summary>
        public double Area => GeometricArc.Area;

        /// <summary>
        /// The Bulge of the Arc
        /// </summary>
        public double Bulge => GeometricArc.BulgeValue;

        /// <summary>
        /// The Geometric Arc
        /// </summary>
        public GeoArc GeometricArc { get; }
    }
}