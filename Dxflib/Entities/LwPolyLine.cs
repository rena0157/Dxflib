// Dxflib
// LwPolyLine.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     The LwPolyLine Entity
    /// </summary>
    public class LwPolyLine : Entity
    {
        #region Constructors

        /// <summary>
        ///     The extraction constructor for the lwpolyline class
        /// </summary>
        /// <param name="lwPolyLineBuffer">A <see cref="LwPolyLineBuffer" /></param>
        public LwPolyLine(LwPolyLineBuffer lwPolyLineBuffer) : base(lwPolyLineBuffer)
        {
            EntityType = lwPolyLineBuffer.EntityType;

            // LwPolyLine Specific
            NumberOfVertices = lwPolyLineBuffer.NumberOfVertices;
            PolyLineFlag = lwPolyLineBuffer.PolyLineFlag;
            ConstantWidth = lwPolyLineBuffer.ConstantWidth;
            Elevation = lwPolyLineBuffer.Elevation;
            Thickness = lwPolyLineBuffer.Thickness;

            // Create the GeoPolyline
            GPolyline = new GeoPolyline(
                lwPolyLineBuffer.XValues, lwPolyLineBuffer.YValues,
                lwPolyLineBuffer.BulgeList, PolyLineFlag);
        }

        #endregion

        #region AutoCADEntityProperties

        /// <summary>
        ///     The Total Number of Vertices in the LwPolyLine
        /// </summary>
        public int NumberOfVertices { get; }

        /// <summary>
        ///     The Polyline Flag which tell you if the polyline is closed or open
        /// </summary>
        public bool PolyLineFlag { get; }

        /// <summary>
        ///     The Constant width or "Global Width"
        /// </summary>
        public double ConstantWidth { get; }

        /// <summary>
        ///     The Elevation of the polyline
        /// </summary>
        public double Elevation { get; }

        /// <summary>
        ///     The Thickness of the LwPolyLine
        /// </summary>
        public double Thickness { get; }

        /// <summary>
        ///     The GeoPolyline Property
        /// </summary>
        public GeoPolyline GPolyline { get; }

        #endregion

        #region GeometricProperties

        /// <summary>
        ///     The Total Length of the LwPolyLine
        /// </summary>
        public double Length => GPolyline.Length;

        /// <summary>
        ///     The Area if the LwPolyLine is Closed
        /// </summary>
        public double Area => GPolyline.Area;

        #endregion
    }
}