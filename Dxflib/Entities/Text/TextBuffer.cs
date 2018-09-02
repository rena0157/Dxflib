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

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dxflib.Annotations;
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
        private int _horizontalJustify;
        private int _verticalJustify;

        /// <inheritdoc />
        /// <summary>
        ///     Defaulting Constructor
        /// </summary>
        public TextBuffer()
        {
            _horizontalJustify = 0;
            _verticalJustify = 0;

            EntityType = typeof(Text);
            Contents = string.Empty;
            IsAnnotative = false;
            Justify = JustifyOptions.Left;
            Height = 0.2;
            Rotation = 0;
            WidthFactor = 1.0;
            Obliquing = 0;
            PositionVertex = new Vertex(0, 0);
            TextStyle = new Style() {Name = "STANDARD"};
            IsUpsideDown = false;
            IsBackwards = false;
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
                    case GroupCodesBase.XPoint:
                        PositionVertex.X = double.Parse(currentData.Value);
                        continue;

                    case GroupCodesBase.YPoint:
                        PositionVertex.Y = double.Parse(currentData.Value);
                        continue;

                    case GroupCodesBase.ZPoint:
                        PositionVertex.Z = double.Parse(currentData.Value);
                        continue;

                    case TextCodes.TextHeight:
                        Height = double.Parse(currentData.Value);
                        continue;

                    case TextCodes.TextString:
                        Contents = currentData.Value;
                        continue;

                    case TextCodes.TextRotation:
                        Rotation = double.Parse(currentData.Value);
                        continue;

                    case TextCodes.RelativeXScale:
                        WidthFactor = double.Parse(currentData.Value);
                        continue;

                    case TextCodes.ObliqueAngle:
                        Obliquing = double.Parse(currentData.Value);
                        continue;

                    case TextCodes.TextStyleName:
                        TextStyle.Name = currentData.Value;
                        continue;

                    case TextCodes.HorizontalJustification:
                        _horizontalJustify = int.Parse(currentData.Value);
                        continue;

                    case TextCodes.VerticalJustification:
                        _verticalJustify = int.Parse(currentData.Value);
                        continue;

                    case TextCodes.TextGenerationFlag:
                        var value = int.Parse(currentData.Value);
                        switch ( value )
                        {
                            case 2:
                                IsBackwards = true;
                                break;
                            case 4:
                                IsUpsideDown = true;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        continue;

                    default:
                        continue;
                }
            }

            Justify = GetJustification(_horizontalJustify, _verticalJustify);
            return true;
        }

        /// <summary>
        /// Get the <see cref="JustifyOptions"/> from horizontal justify and vertical justify
        /// </summary>
        /// <param name="horizontalJustify">The horizontal Justification</param>
        /// <param name="verticalJustify">The Vertical Justification</param>
        /// <returns>A <see cref="JustifyOptions"/> that matches horizontal and vertical Justify</returns>
        private static JustifyOptions GetJustification(int horizontalJustify, int verticalJustify)
        {
            switch ( verticalJustify )
            {
                case 0 when horizontalJustify == 0:
                    return JustifyOptions.Left;

                case 0 when horizontalJustify == 1:
                    return JustifyOptions.Center;
                
                case 0 when horizontalJustify == 2:
                    return JustifyOptions.Right;
                
                case 0 when horizontalJustify == 3:
                    return JustifyOptions.Aligned;
                
                case 0 when horizontalJustify == 4:
                    return JustifyOptions.Middle;
                
                case 0 when horizontalJustify == 5:
                    return JustifyOptions.Fit;
                
                case 1 when horizontalJustify == 0:
                    return JustifyOptions.BottomLeft;
                
                case 1 when horizontalJustify == 1:
                    return JustifyOptions.BottomCenter;
                
                case 1 when horizontalJustify == 2:
                    return JustifyOptions.BottomRight;
                
                case 2 when horizontalJustify == 0:
                    return JustifyOptions.MiddleLeft;
                
                case 2 when horizontalJustify == 1:
                    return JustifyOptions.MiddleCenter;
                
                case 2 when horizontalJustify == 2:
                    return JustifyOptions.MiddleRight;
                
                case 3 when horizontalJustify == 0:
                    return JustifyOptions.TopLeft;

                case 3 when horizontalJustify == 1:
                    return JustifyOptions.TopCenter;

                case 3 when horizontalJustify == 2:
                    return JustifyOptions.TopRight;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}