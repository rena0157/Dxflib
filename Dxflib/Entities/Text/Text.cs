using System;
using System.Collections.Generic;
using System.Text;
using Dxflib.Geometry;

namespace Dxflib.Entities.Text
{
    public class Text : Entity, IText
    {
        public Text(TextBuffer tb)
        {
            
        }

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
