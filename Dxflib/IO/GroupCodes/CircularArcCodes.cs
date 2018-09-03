// Dxflib
// CircularArcCodes.cs
// 
// ============================================================
// 
// Created: 2018-09-02
// Last Updated: 2018-09-03-12:08 PM
// By: Adam Renaud
// 
// ============================================================

namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    ///     Circular Arc Group Codes.
    /// </summary>
    public abstract class CircularArcCodes : GroupCodesBase
    {
        /// <summary>
        ///     The <see cref="Dxflib.Entities.CircularArc"/> Entity Start Marker
        /// </summary>
        public const string ArcStartMarker = "ARC";

        /// <summary>
        /// The <see cref="Dxflib.Entities.Circle"/> Entity Start Marker
        /// </summary>
        public const string CircleStartMarker = "CIRCLE";

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