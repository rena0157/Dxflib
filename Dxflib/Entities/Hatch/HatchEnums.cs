// Dxflib
// HatchEnums.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.IO;
using Dxflib.IO.GroupCodes;

namespace Dxflib.Entities.Hatch
{
    /// <summary>
    ///     Hatch Styles. <see cref="HatchCodes.HatchStyle" />
    /// </summary>
    public enum HatchStyles
    {
        /// <summary>
        ///     Hatch "odd parity" area
        /// </summary>
        Normal,

        /// <summary>
        ///     Hatch outermost area
        /// </summary>
        Outer,

        /// <summary>
        ///     Hatch through entire area
        /// </summary>
        Ignore
    }

    /// <summary>
    ///     Hatch Pattern Type. <see cref="HatchCodes.HatchPatternType" />
    /// </summary>
    public enum HatchPatternType
    {
        /// <summary>
        ///     User Defined Hatch Pattern
        /// </summary>
        UserDefined,

        /// <summary>
        ///     A predefined Hatch Pattern
        /// </summary>
        Predefined,

        /// <summary>
        ///     A Custom Hatch Pattern
        /// </summary>
        Custom
    }

    // Boundary Data Objects ----------------------------

    /// <summary>
    ///     Hatch Edge type. <see cref="HatchCodes.EdgeType" />
    /// </summary>
    public enum EdgeTypes
    {
        /// <summary>
        ///     A line Edge Type. <see cref="Dxflib.Geometry.GeoLine" />
        /// </summary>
        Line = 1,

        /// <summary>
        ///     A Circular Arc. <see cref="Dxflib.Geometry.GeoArc" />
        /// </summary>
        CircularArc = 2,

        /// <summary>
        /// </summary>
        EllipticalArc = 3, // Todo Add this edge type information

        /// <summary>
        /// </summary>
        Spline = 4 // Todo Add this edge type information
    }
}