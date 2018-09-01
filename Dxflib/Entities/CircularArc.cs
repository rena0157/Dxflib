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
        private readonly GeoArc _geoArc;

        /// <inheritdoc />
        /// <summary>
        ///     The <see cref="T:Dxflib.Entities.CircularArcBuffer" /> constructor
        ///     for the Arc Entity
        /// </summary>
        /// <param name="ab">The <see cref="T:Dxflib.Entities.CircularArcBuffer" /></param>
        public CircularArc(CircularArcBuffer ab) : base(ab)
        {
            EntityType = EntityTypes.CircularArc;
            _geoArc = new GeoArc(
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
        public Vertex CenterPoint => _geoArc.CenterVertex;

        /// <summary>
        ///     The Middle Vertex of the Arc
        /// </summary>
        public Vertex MiddleVertex => _geoArc.MiddleVertex;

        /// <summary>
        /// The Starting Vertex
        /// </summary>
        public Vertex StartingVertex => _geoArc.Vertex0;

        /// <summary>
        /// The Ending Vertex
        /// </summary>
        public Vertex EndingVertex => _geoArc.Vertex1;

        /// <summary>
        /// The Starting Angle of the Arc
        /// </summary>
        public double StartAngle => _geoArc.StartAngle;

        /// <summary>
        /// The End Angle of the Arc
        /// </summary>
        public double EndAngle => _geoArc.EndAngle;

        /// <summary>
        /// The Radius of the Arc
        /// </summary>
        public double Radius => _geoArc.Radius;

        /// <summary>
        /// The Arc Length of the Arc
        /// </summary>
        public double Length => _geoArc.Length;

        /// <summary>
        /// The Area of the Arc
        /// </summary>
        public double Area => _geoArc.Area;

        /// <summary>
        /// The Bulge of the Arc
        /// </summary>
        public double Bulge => _geoArc.BulgeValue;

    }
}