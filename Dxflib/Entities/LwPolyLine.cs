// Dxflib
// LwPolyLine.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-08-07-11:04 AM
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
        // The Geometric backing class
        private readonly GeoPolyline _geoPolyline;

        #region Constructors

        /// <summary>
        ///     The Main Extraction Constructor for the LwPolyLine Class
        /// </summary>
        /// <param name="lwPolyLineBuffer"></param>
        public LwPolyLine(LwPolyLineBuffer lwPolyLineBuffer)
        {
            // Entity Base
            EntityType = EntityTypes.Lwpolyline;
            LayerNameBf = lwPolyLineBuffer.LayerName;
            Handle = lwPolyLineBuffer.Handle;

            // LwPolyLine Specific
            NumberOfVerticies = lwPolyLineBuffer.NumberOfVerticies;
            PolyLineFlag = lwPolyLineBuffer.PolyLineFlag;
            ConstantWidth = lwPolyLineBuffer.ConstantWidth;
            Elevation = lwPolyLineBuffer.Elevation;
            Thickness = lwPolyLineBuffer.Thickness;

            // Create the GeoPolyline
            _geoPolyline = new GeoPolyline(
                lwPolyLineBuffer.XValues, lwPolyLineBuffer.YValues,
                lwPolyLineBuffer.BulgeList, PolyLineFlag);
        }

        #endregion

        #region AutoCADEntityProperties

        /// <summary>
        ///     The Total Number of Verticies in the LwPolyLine
        /// </summary>
        public int NumberOfVerticies { get; }

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

        #endregion

        #region GeometricProperties

        /// <summary>
        ///     The Total Length of the LwPolyLine
        /// </summary>
        public double Length => _geoPolyline.Length;

        /// <summary>
        ///     The Area if the LwPolyLine is Closed
        /// </summary>
        public double Area { get; }

        #endregion
    }
}