// Dxflib
// TextBuffer.cs
// 
// ============================================================
// 
// Created: 2018-09-01
// Last Updated: 2018-09-02-10:40 AM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Geometry;
using Dxflib.IO;
using Dxflib.IO.GroupCodes;

namespace Dxflib.Entities.Text
{
    /// <inheritdoc cref="EntityBuffer" />
    /// <inheritdoc cref="IText" />
    /// <summary>
    ///     The Text Buffer, used to build <see cref="T:Dxflib.Entities.Text.Text" /> Entities
    /// </summary>
    public class TextBuffer : EntityBuffer, IText
    {
        /// <inheritdoc />
        /// <summary>
        ///     Defaulting Constructor
        /// </summary>
        public TextBuffer()
        {
            EntityType = EntityTypes.Text;
            Contents = string.Empty;
            IsAnnotative = false;
            Justify = JustifyOptions.Left;
            Height = 0.2;
            Rotation = 0;
            WidthFactor = 1.0;
            Obliquing = 0;
            PositionVertex = new Vertex(0, 0);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public string Contents { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public Style TextStyle { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool IsAnnotative { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public JustifyOptions Justify { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public double Height { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public double Rotation { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public double WidthFactor { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public double Obliquing { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public Vertex PositionVertex { get; set; }

        /// <summary>
        /// true if text is backwards
        /// </summary>
        public bool IsBackwards { get; set; }

        /// <summary>
        /// True if text is upside down
        /// </summary>
        public bool IsUpsideDown { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     The <see cref="T:Dxflib.Entities.Text.TextBuffer" /> Parse Function
        /// </summary>
        /// <param name="list"><see cref="T:Dxflib.IO.TaggedDataList" /> list</param>
        /// <param name="index">The current index of extraction</param>
        /// <returns>Always True</returns>
        public override bool Parse(TaggedDataList list, int index)
        {
            for ( var currentIndex = index + 1; currentIndex < list.Length; ++currentIndex )
            {
                var currentData = list.GetPair(currentIndex);

                if ( currentData.GroupCode == GroupCodesBase.EntityType )
                    break;

                if ( base.Parse(list, currentIndex) )
                    continue;

                switch ( currentData.GroupCode )
                {
                    default:
                        continue;
                }
            }

            return true;
        }
    }
}