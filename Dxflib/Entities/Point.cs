// Dxflib
// Point.cs
// 
// ============================================================
// 
// Created: 2018-09-03
// Last Updated: 2018-09-03-11:35 AM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Point Entity
    /// </summary>
    public class Point : Entity
    {
        private readonly Vertex _vertex;

        /// <inheritdoc />
        /// <summary>
        /// Buffer Constructor
        /// </summary>
        /// <param name="pb">The Point Buffer</param>
        public Point(PointBuffer pb) : base(pb)
        {
            EntityType = pb.EntityType;
            _vertex = new Vertex(pb.X, pb.Y, pb.Z);
        }

        /// <summary>
        /// The X Position of the Point
        /// </summary>
        public double X => _vertex.X;

        /// <summary>
        /// The Y Position of the Point
        /// </summary>
        public double Y => _vertex.Y;

        /// <summary>
        /// The Z Position of the Point
        /// </summary>
        public double Z => _vertex.Z;
    }
}