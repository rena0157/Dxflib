namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    ///     The HatchGroupCodes. <see cref="Dxflib.Entities.Hatch" />
    /// </summary>
    public abstract class HatchCodes : GroupCodesBase
    {
        /// <summary>
        ///     The Start Marker of the Hatch Entity
        /// </summary>
        public const string StartMarker = "HATCH";

        /// <summary>
        ///     Hatch pattern name
        /// </summary>
        public const string HatchPatternName = "  2";

        /// <summary>
        ///     Solid fill flag (0 = pattern fill; 1 = solid fill);
        ///     for MPolygon, the version of MPolygon
        /// </summary>
        public const string SolidFillFlag = " 70";

        /// <summary>
        ///     Associativity flag (0 = non-associative; 1 = associative);
        ///     for MPolygon, solid-fill flag (0 = lacks solid fill; 1 = has solid fill)
        /// </summary>
        public const string AssociativityFlag = " 71";

        /// <summary>
        ///     Number of boundary paths (loops)
        /// </summary>
        public const string NumberOfBoundaryLoops = " 91";

        /// <summary>
        ///     Hatch style:
        ///     0 = Hatch “odd parity” area (Normal style)
        ///     1 = Hatch outermost area only (Outer style)
        ///     2 = Hatch through entire area (Ignore style)
        /// </summary>
        public const string HatchStyle = " 75";

        /// <summary>
        ///     Hatch Pattern type
        ///     0 = User defined
        ///     1 = Predefined
        ///     2 = Custom
        /// </summary>
        public const string HatchPatternType = " 76";

        /// <summary>
        ///     Hatch Pattern Angle (pattern fill only)
        /// </summary>
        public const string HatchPatternAngle = " 52";

        /// <summary>
        ///     Hatch Pattern Scale or spacing (pattern fill only)
        /// </summary>
        public const string HatchPatternScale = " 41";

        /// <summary>
        ///     Number of Pattern Definition Lines
        /// </summary>
        public const string NumberOfPatternDefLines = " 78";

        // Boundary Data -----------------------------

        /// <summary>
        ///     Number of edges in this boundary path (only if boundary is not a polyline)
        /// </summary>
        public const string NumberOfEdgesInBoundary = " 93";

        /// <summary>
        ///     Edge Type (Only if boundary is not a polyline)
        ///     1 = Line
        ///     2 = Circular Arc
        ///     3 = Elliptical Arc
        ///     4 = Spline
        /// </summary>
        public const string EdgeType = " 72";

        /// <summary>
        ///     Number of source objects, <see cref="GroupCodesBase.SoftPointer" />
        /// </summary>
        public const string SourceObjectsCount = " 97";
    }
}