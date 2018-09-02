// Dxflib
// MText.cs
// 
// ============================================================
// 
// Created: 2018-09-02
// Last Updated: 2018-09-02-5:57 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;
using Dxflib.Geometry;

namespace Dxflib.Entities.Text
{
    /// <inheritdoc cref="Entity" />
    /// <inheritdoc cref="IText" />
    /// <summary>
    ///     Multiline Text
    /// </summary>
    public class MText : Entity, IText
    {
        /// <inheritdoc />
        /// <summary>
        ///     Multiline Text Constructor
        /// </summary>
        /// <param name="tb"></param>
        public MText(MTextBuffer tb) : base(tb)
        {
            EntityType = typeof(MText);
            Contents = tb.Contents;
            TextStyle = tb.TextStyle;
            IsAnnotative = tb.IsAnnotative;
            Justify = tb.Justify;
            Height = tb.Height;
            Rotation = tb.Rotation;
            PositionVertex = tb.PositionVertex;
            DrawDirection = tb.DrawDirection;
            ReferenceRectangleWidth = tb.ReferenceRecWidth;
        }

        /// <summary>
        ///     Draw Direction of the text
        /// </summary>
        public DrawDirections DrawDirection { get; set; }

        /// <summary>
        ///     The width of the reference rectangle
        /// </summary>
        public double ReferenceRectangleWidth { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Property Changed Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        /// <summary>
        ///     Contents of the string
        /// </summary>
        public string Contents { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Text Style
        /// </summary>
        public Style TextStyle { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Is annotative Flag
        /// </summary>
        public bool IsAnnotative { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Justify Options
        /// </summary>
        public JustifyOptions Justify { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Height of the text
        /// </summary>
        public double Height { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Rotation of the text in radians
        /// </summary>
        public double Rotation { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Position Vector of the text
        /// </summary>
        public Vertex PositionVertex { get; set; }
    }
}