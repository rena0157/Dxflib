using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dxflib.Geometry;

namespace Dxflib.Entities.Text
{
    /// <inheritdoc />
    /// <summary>
    /// Public interface for Text Entities
    /// </summary>
    public interface IText : INotifyPropertyChanged
    {
        /// <summary>
        /// The String Contents
        /// </summary>
        string Contents { get; set; }

        /// <summary>
        /// The Text Style <see cref="Style"/>
        /// </summary>
        Style TextStyle { get; set; }

        /// <summary>
        /// True if is annotative
        /// </summary>
        bool IsAnnotative { get; set; }

        /// <summary>
        /// The Justify Option <see cref="JustifyOptions"/>
        /// </summary>
        JustifyOptions Justify { get; set; }

        /// <summary>
        /// The Text Height
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// The Rotation of the text
        /// </summary>
        double Rotation { get; set; }

        /// <summary>
        /// The Position Vertex of the text
        /// </summary>
        Vertex PositionVertex { get; set; }
    }
}
