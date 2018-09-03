// Dxflib
// Circle.cs
// 
// ============================================================
// 
// Created: 2018-09-03
// Last Updated: 2018-09-03-12:27 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     Circle Entity
    /// </summary>
    public class Circle : Entity
    {
        /// <inheritdoc />
        /// <summary>
        ///     Circle Buffer Constructor
        /// </summary>
        /// <param name="cb">A <see cref="CircularArcBuffer" /> Buffer</param>
        /// <remarks>
        ///     Note that only the Radius and CenterPoint information will be used from the
        ///     Buffer
        /// </remarks>
        public Circle(CircularArcBuffer cb) : base(cb)
        {
            EntityType = typeof(Circle);
            CenterPoint = new Vertex(cb.CenterPointX, cb.CenterPointY, cb.CenterPointZ);
            Radius = cb.Radius;
        }

        /// <summary>
        ///     Center Point of the Circle
        /// </summary>
        public Vertex CenterPoint { get; set; }

        /// <summary>
        ///     The Radius of the Circle
        /// </summary>
        public double Radius { get; set; }
    }
}