namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    ///     Circular Arc Group Codes.
    /// </summary>
    public abstract class CircularArcCodes : GroupCodesBase
    {
        /// <summary>
        ///     The Start Marker of the Arc Entity
        /// </summary>
        public const string StartMarker = "ARC";

        /// <summary>
        ///     Thickness (optional; default = 0)
        /// </summary>
        public const string Thickness = " 39";

        /// <summary>
        ///     The Radius of the Arc
        /// </summary>
        public const string Radius = " 40";

        /// <summary>
        ///     The Starting Angle of the Arc (Degrees)
        /// </summary>
        public const string StartAngle = " 50";

        /// <summary>
        ///     The Ending Angle of the Arc (Degrees)
        /// </summary>
        public const string EndAngle = " 51";

        /// <summary>
        ///     True if the arc is counter clock wise
        /// </summary>
        public const string IsCounterClockWise = " 73";
    }
}