// Dxflib
// HeaderSectionArgs.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;
// ReSharper disable InconsistentNaming

namespace Dxflib.IO.Header
{
    /// <summary>
    ///     AutoCAD Versions up to 2013
    /// </summary>
    public enum AutoCadVersions
    {
        /// <summary>
        ///     AutoCAD Version R10
        /// </summary>
        [Description("R10")] AC1006,

        /// <summary>
        ///     AutoCAD version R11 and R12
        /// </summary>
        [Description("R11 and R12")] AC1009,

        /// <summary>
        ///     AutoCAD Version R13
        /// </summary>
        [Description("R13")] AC1012,

        /// <summary>
        ///     AutoCAD Version R14
        /// </summary>
        [Description("R14")] AC1014,

        /// <summary>
        ///     AutoCAD Version 2000
        /// </summary>
        [Description("AutoCAD 2000")] AC1015,

        /// <summary>
        ///     AutoCAD Version 2004
        /// </summary>
        [Description("AutoCAD 2004")] AC1018,

        /// <summary>
        ///     AutoCAD Version 2007
        /// </summary>
        [Description("AutoCAD 2007")] AC1021,

        /// <summary>
        ///     AutoCAD Version 2010
        /// </summary>
        [Description("AutoCAD 2010")] AC1024,

        /// <summary>
        ///     AutoCAD Version 2013
        /// </summary>
        [Description("AutoCAD 2013")] AC1027,

        /// <summary>
        ///     The AutoCAD Version is Unknown
        /// </summary>
        [Description("Unknown")] Unknown
    }
}