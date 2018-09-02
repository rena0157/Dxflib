using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dxflib.Geometry;

namespace Dxflib.Entities.Text
{
    /// <inheritdoc />
    /// <summary>
    /// Multiline Text
    /// </summary>
    public class MText : Entity, IText
    {
        /// <inheritdoc />
        /// <summary>
        /// Multiline Text Constructor
        /// </summary>
        /// <param name="tb"></param>
        public MText(MTextBuffer tb) : base(tb)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Contents { get; set; }
        public Style TextStyle { get; set; }
        public bool IsAnnotative { get; set; }
        public JustifyOptions Justify { get; set; }
        public double Height { get; set; }
        public double Rotation { get; set; }
        public double WidthFactor { get; set; }
        public double Obliquing { get; set; }
        public Vertex PositionVertex { get; set; }
    }
}
