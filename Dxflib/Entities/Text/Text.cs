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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dxflib.Annotations;
using Dxflib.Geometry;

namespace Dxflib.Entities.Text
{
    /// <inheritdoc cref="Entity" />
    /// <inheritdoc cref="IText" />
    /// <summary>
    /// </summary>
    public sealed class Text : Entity, IText
    {
        private Vertex _positionVertex;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="tb"></param>
        public Text(TextBuffer tb) : base(tb)
        {
            IsBackwards = tb.IsBackwards;
            IsUpsideDown = tb.IsUpsideDown;
            Contents = tb.Contents;
            TextStyle = tb.TextStyle;
            IsAnnotative = tb.IsAnnotative;
            Justify = tb.Justify;
            Height = tb.Height;
            Rotation = tb.Rotation;
            WidthFactor = tb.WidthFactor;
            Obliquing = tb.Obliquing;
            _positionVertex = tb.PositionVertex;
        }

        /// <summary>
        /// True if the text is rendered backwards
        /// </summary>
        public bool IsBackwards { get; set; }

        /// <summary>
        /// True if the text is UpsideDown
        /// </summary>
        public bool IsUpsideDown { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The Contents of the string
        /// </summary>
        public string Contents { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The <see cref="P:Dxflib.Entities.Text.Text.TextStyle" /> of the entity
        /// </summary>
        public Style TextStyle { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// True if the text is annotative
        /// </summary>
        public bool IsAnnotative { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The <see cref="T:Dxflib.Entities.Text.JustifyOptions" /> Option
        /// </summary>
        public JustifyOptions Justify { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The Height of the text
        /// </summary>
        public double Height { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The rotation of the text (Degrees)
        /// </summary>
        public double Rotation { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The width Factor
        /// </summary>
        public double WidthFactor { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The obliquing of the text (Italics)
        /// </summary>
        public double Obliquing { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The Position Vector of the text
        /// </summary>
        public Vertex PositionVertex
        {
            get => _positionVertex;
            set
            {
                _positionVertex = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Public Event that occurs if a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The Invoking of a property changed event
        /// </summary>
        /// <param name="propertyName">The Property Name</param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}