using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.Entities.Text
{
    /// <summary>
    /// DrawDirection Options
    /// </summary>
    public enum DrawDirections
    {
        /// <summary>
        /// Left to right
        /// </summary>
        LeftToRight,

        /// <summary>
        /// Top to Bottom
        /// </summary>
        TopToBottom,

        /// <summary>
        /// By Style, Draw direction is inherited by the style
        /// </summary>
        ByStyle
    }
}
