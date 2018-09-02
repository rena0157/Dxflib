// Dxflib
// Text.cs
// 
// ============================================================
// 
// Created: 2018-09-01
// Last Updated: 2018-09-02-10:34 AM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;

namespace Dxflib.Entities.Text
{
    /// <inheritdoc cref="Entity" />
    /// <inheritdoc cref="IText" />
    /// <summary>
    /// </summary>
    public class Text : Entity, IText
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="tb"></param>
        public Text(TextBuffer tb) : base(tb)
        {
            Contents = tb.Contents;
            TextStyle = tb.TextStyle;
            IsAnnotative = tb.IsAnnotative;
            Justify = tb.Justify;
            Height = tb.Height;
            Rotation = tb.Rotation;
            WidthFactor = tb.WidthFactor;
            Obliquing = tb.Obliquing;
            PositionVertex = tb.PositionVertex;
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