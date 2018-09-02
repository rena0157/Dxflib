// Dxflib
// MTextBuffer.cs
// 
// ============================================================
// 
// Created: 2018-09-02
// Last Updated: 2018-09-02-5:39 PM
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
    ///     The <see cref="T:Dxflib.Entities.Text.MText" /> Buffer Class
    /// </summary>
    public class MTextBuffer : EntityBuffer, IText
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public MTextBuffer()
        {
            EntityType = typeof(MText);
            PositionVertex = new Vertex(0, 0);
            Height = 0.2;
            ReferenceRecWidth = 0;
            Contents = string.Empty;
            TextStyle = new Style {Name = "STANDARD"};
            DrawDirection = DrawDirections.ByStyle;
            BackgroundFillSetting = BackgroundFillSettings.Off;
        }

        /// <summary>
        ///     <see cref="DrawDirections" /> setting
        /// </summary>
        public DrawDirections DrawDirection { get; set; }

        /// <summary>
        ///     The Background fill setting
        /// </summary>
        public BackgroundFillSettings BackgroundFillSetting { get; set; }

        /// <summary>
        ///     The Width of the reference rectangle
        /// </summary>
        public double ReferenceRecWidth { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Property Changed Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        /// <summary>
        ///     The Contents of the text
        /// </summary>
        public string Contents { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Text Style
        /// </summary>
        public Style TextStyle { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Returns True if the text is annotative
        /// </summary>
        public bool IsAnnotative { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Also known as the attachment point
        /// </summary>
        public JustifyOptions Justify { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Height of the MText
        /// </summary>
        public double Height { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Rotation in Radians
        /// </summary>
        public double Rotation { get; set; }


        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public Vertex PositionVertex { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Parsing function of the MText Object
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
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
                        break;

                    case GroupCodesBase.YPoint:
                        PositionVertex.Y = double.Parse(currentData.Value);
                        break;

                    case GroupCodesBase.ZPoint:
                        PositionVertex.Z = double.Parse(currentData.Value);
                        break;

                    case TextCodes.TextHeight:
                        Height = double.Parse(currentData.Value);
                        break;

                    case MTextCodes.ReferenceRectangleWidth:
                        ReferenceRecWidth = double.Parse(currentData.Value);
                        continue;

                    case MTextCodes.AttachmentPoint:
                        Justify = ParseAttachmentPoint(int.Parse(currentData.Value));
                        continue;

                    case MTextCodes.DrawDirection:
                        var value = int.Parse(currentData.Value);
                        switch ( value )
                        {
                            case 1:
                                DrawDirection = DrawDirections.LeftToRight;
                                break;
                            case 3:
                                DrawDirection = DrawDirections.TopToBottom;
                                break;
                            case 5:
                                DrawDirection = DrawDirections.ByStyle;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;

                    case TextCodes.TextString:
                        Contents = currentData.Value;
                        break;

                    case MTextCodes.AdditionalText:
                        Contents += currentData.Value;
                        break;

                    case TextCodes.TextStyleName:
                        TextStyle.Name = currentData.Value;
                        break;

                    default:
                        continue;
                }
            }

            return true;
        }

        /// <summary>
        ///     Property Changed Invocation
        /// </summary>
        /// <param name="propertyName">The Property Name</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static JustifyOptions ParseAttachmentPoint(int attachmentPoint)
        {
            switch ( attachmentPoint )
            {
                case 1:
                    return JustifyOptions.TopLeft;
                case 2:
                    return JustifyOptions.TopCenter;
                case 3:
                    return JustifyOptions.TopRight;
                case 4:
                    return JustifyOptions.MiddleLeft;
                case 5:
                    return JustifyOptions.MiddleCenter;
                case 6:
                    return JustifyOptions.MiddleRight;
                case 7:
                    return JustifyOptions.BottomLeft;
                case 8:
                    return JustifyOptions.BottomCenter;
                case 9:
                    return JustifyOptions.BottomRight;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}