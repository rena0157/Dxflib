// Dxflib
// HatchEnums.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-08-26-4:50 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.IO;

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
    /// Hatch Pattern Type. <see cref="HatchCodes.HatchPatternType"/>
    /// </summary>
    public enum HatchPatternType
    {
        /// <summary>
        /// User Defined Hatch Pattern
        /// </summary>
        UserDefined,

        /// <summary>
        /// A predefined Hatch Pattern
        /// </summary>
        Predefined,

        /// <summary>
        /// A Custom Hatch Pattern
        /// </summary>
        Custom
    }
}