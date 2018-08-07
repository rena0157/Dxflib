// Dxflib
// GeoMath.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-07-10:40 AM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.Geometry
{
    /// <summary>
    ///     A Static Class that contains static method for calcuating geometric
    ///     values.
    /// </summary>
    public static class GeoMath
    {
        /// <summary>
        ///     The Global tolerance for comparing double values
        /// </summary>
        public const double Tolerance = 0.001;

        /// <summary>
        ///     Calculates the direct distance between two verticies using
        ///     Pythagoean Theorem.
        /// </summary>
        /// <param name="v0">The First Vertex</param>
        /// <param name="v1">The Second Vertex</param>
        /// <returns>The Distance between the two points</returns>
        public static double Distance(Vertex v0, Vertex v1)
        {
            return Math.Sqrt(Math.Pow(v1.X - v0.X, 2) +
                             Math.Pow(v1.Y - v0.Y, 2)) +
                   Math.Pow(v0.Z - v1.Z, 2);
        }

        /// <summary>
        ///     Calculates the distance between two points as it follows
        ///     an arc that is defined by a bulge.
        /// </summary>
        /// <param name="v0">The First Vertex</param>
        /// <param name="v1">The Second Vertex</param>
        /// <param name="bulge">The Bulge</param>
        /// <returns>
        ///     A double values that represents the length
        ///     along the arc defined by the two points and
        ///     the Bulge
        /// </returns>
        public static double Distance(Vertex v0, Vertex v1, double bulge)
        {
            return Bulge.Length(Bulge.Radius(v0, v1, Bulge.Angle(bulge)), Bulge.Angle(bulge));
        }
    }
}