// Dxflib
// EntityTypes.cs
// 
// ============================================================
// 
// Created: 2018-08-30
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;

namespace Dxflib.Entities
{
    /// <summary>
    ///     All of the different entity types that are currently supported for extraction
    /// </summary>
    public enum EntityTypes
    {
        /// <summary>
        ///     <see cref="Dxflib.Entities.Line" />
        /// </summary>
        [Description("LINE")] Line,

        /// <summary>
        ///     <see cref="Dxflib.Entities.LwPolyLine" />
        /// </summary>
        [Description("LWPOLYLINE")] Lwpolyline,

        /// <summary>
        ///     <see cref="Dxflib.Entities.Hatch" />
        /// </summary>
        [Description("HATCH")] Hatch,

        /// <summary>
        ///     <see cref="Dxflib.Entities.CircularArc" />
        /// </summary>
        [Description("ARC")] CircularArc,

        /// <summary>
        /// <see cref="Dxflib.Entities.Text.Text"/>
        /// </summary>
        [Description("TEXT")] Text,

        /// <summary>
        ///     No specific Entity
        /// </summary>
        [Description("Not Set")] None
    }
}