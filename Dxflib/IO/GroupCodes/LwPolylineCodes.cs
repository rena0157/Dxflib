namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    ///     The <see cref="T:Dxflib.Entities.LwPolyLine" /> entity group codes
    /// </summary>
    public abstract class LwPolylineCodes : GroupCodesBase
    {
        /// <summary>
        ///     The Starting marker of the entity
        /// </summary>
        public const string StartMarker = "LWPOLYLINE";

        /// <summary>
        ///     Number of vertices
        /// </summary>
        public const string NumberOfVertices = " 90";

        /// <summary>
        ///     Polyline flag (bit-coded); default is 0: 1 = Closed
        /// </summary>
        public const string PolylineFlag = " 70";

        /// <summary>
        ///     Constant width (optional; default = 0).
        ///     Not used if variable width (codes 40 and/or 41) is set
        /// </summary>
        public const string ConstantWidth = " 43";

        /// <summary>
        ///     Elevation (optional; default = 0)
        /// </summary>
        public const string Elevation = " 38";

        /// <summary>
        ///     Thickness (optional; default = 0)
        /// </summary>
        public new const string Thickness = " 39";

        /// <summary>
        ///     The Vertex Identifier
        /// </summary>
        public const string VertexIdentifier = " 91";

        /// <summary>
        ///     Starting width (multiple entries; one entry for each vertex)
        ///     (optional; default = 0; multiple entries). Not used if constant width (code 43) is set
        /// </summary>
        public const string StartingWidth = " 40";

        /// <summary>
        ///     End width (multiple entries; one entry for each vertex)
        ///     (optional; default = 0; multiple entries). Not used if constant width (code 43) is set
        /// </summary>
        public const string EndingWith = " 41";

        /// <summary>
        ///     Bulge (multiple entries; one entry for each vertex) (optional; default = 0)
        /// </summary>
        public const string Bulge = " 42";
    }
}