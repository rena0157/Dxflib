using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    /// The <see cref="T:Dxflib.Entities.Point" /> Entity Group Codes
    /// </summary>
    public abstract class PointCodes : GroupCodesBase
    {
        /// <summary>
        /// The Point Starting Marker
        /// </summary>
        public const string StartMarker = "POINT";

        /// <summary>
        /// The Angle of the X axis for the UCS in effect when the
        /// point was drawn (optional, default = 0);
        /// </summary>
        /// <remarks>
        /// Used when PD-MODE is zero
        /// </remarks>
        public const string AngleOfXAxis = " 50";
    }
}
