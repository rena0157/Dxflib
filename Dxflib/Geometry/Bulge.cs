// Dxflib
// Bulge.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-07-10:40 AM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.Geometry
{
    internal struct Bulge
    {
        /// <summary>
        ///     The Bulge Null Value
        /// </summary>
        public const double BulgeNull = -2.0;

        /// <summary>
        ///     The Total Angle between the two arms of the arc
        /// </summary>
        /// <param name="bulgeValue">The bulge value</param>
        /// <returns>The Total Angle between the two arms to the arc in Radians</returns>
        public static double Angle(double bulgeValue)
        {
            return 4 * Math.Atan(Math.Abs(bulgeValue));
        }

        public static double CalcBulge(double value) { return Math.Tan(value / 4); }

        /// <summary>
        ///     The Radius of the Bulge
        /// </summary>
        /// <param name="v0">The Stating Vertex</param>
        /// <param name="v1">The Ending Vertex</param>
        /// <param name="angle">The Total Angle of the arc</param>
        /// <returns></returns>
        public static double Radius(Vertex v0, Vertex v1, double angle)
        {
            return GeoMath.Distance(v0, v1) / ( 2 * Math.Sin(angle / 2) );
        }

        /// <summary>
        ///     The Arc Length of the bulge
        /// </summary>
        /// <param name="radius">The Radius of the bulge</param>
        /// <param name="angle">The Angle of the bulge in radians</param>
        /// <returns></returns>
        public static double Length(double radius, double angle)
        {
            return radius * angle;
        }

        public static double AngleFromRadius(Vertex vertex0, Vertex vertex1, double radius) { return 0; }
    }
}