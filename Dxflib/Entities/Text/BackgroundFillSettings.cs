// Dxflib
// BackgroundFillSettings.cs
// 
// ============================================================
// 
// Created: 2018-09-02
// Last Updated: 2018-09-02-2:38 PM
// By: Adam Renaud
// 
// ============================================================

namespace Dxflib.Entities.Text
{
    /// <summary>
    ///     The different background fill settings
    /// </summary>
    public enum BackgroundFillSettings
    {
        /// <summary>
        ///     The Background fill is off
        /// </summary>
        Off,

        /// <summary>
        ///     Use a fill color
        /// </summary>
        FillColor,

        /// <summary>
        ///     Use the drawing color
        /// </summary>
        DrawingColor
    }
}