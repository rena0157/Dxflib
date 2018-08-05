using System;

namespace Dxflib.Geometry
{
    public static class GeoMath
    {
        /// <summary>
        ///     The Global tolerance for comparing double values
        /// </summary>
        public const double Tolerance = 0.001;

        public static double Distance(Vertex v0, Vertex v1)
        {
            return Math.Sqrt(Math.Pow(v1.X - v0.X, 2) +
                             Math.Pow(v1.Y - v0.Y, 2)) +
                   Math.Pow(v0.Z - v1.Z, 2);
        }
    }
}