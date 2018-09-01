// Dxflib
// Vertex.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.Geometry
{
    /// <inheritdoc />
    /// <summary>
    ///     The Vertex Class
    /// </summary>
    public class Vertex : GeometricEntityBase
    {
        private double _x;
        private double _y;
        private double _z;

        /// <summary>
        ///     Vertex Constructor
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        public Vertex(double x, double y, double z = 0)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        ///     x coordinate, note that when setting this value
        ///     the <see cref="GeometryChangedHandler" /> Event will broadcast
        /// </summary>
        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("X"));
            }
        }

        /// <summary>
        ///     y coodinate, note that when setting to a new value
        ///     a geometry event will be raised
        /// </summary>
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Y"));
            }
        }

        /// <summary>
        ///     z coordinate, note that when setting to a new value
        ///     a geometry event will be raised
        /// </summary>
        public double Z
        {
            get => _z;
            set
            {
                _z = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Z"));
            }
        }

        /// <summary>
        ///     Returns true if the object can be cast to a vertex and
        ///     the points are the same.
        /// </summary>
        /// <param name="obj">The input object</param>
        /// <returns>True if the vertices are the same point</returns>
        public override bool Equals(object obj)
        {
            if ( obj == null ) return false;

            if ( !( obj is Vertex vertex ) ) return false;

            return Equals(vertex);
        }

        /// <summary>
        ///     Gets the hash of all the elements
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _x.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ _y.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ _z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Equals function override for a vertex input.
        ///     Two Vertices are Equal if all components (x, y, z) are the same
        ///     within the <see cref="GeoMath.Tolerance" />.
        /// </summary>
        /// <param name="vertex">The Vertex to be compared to</param>
        /// <returns>True if the vertices are the same point</returns>
        public bool Equals(Vertex vertex)
        {
            return Math.Abs(X - vertex.X) < GeoMath.Tolerance &&
                   Math.Abs(Y - vertex.Y) < GeoMath.Tolerance &&
                   Math.Abs(Z - vertex.Z) < GeoMath.Tolerance;
        }
    }
}