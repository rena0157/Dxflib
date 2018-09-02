using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dxflib.Geometry;
using Dxflib.IO;
using Dxflib.IO.GroupCodes;

namespace Dxflib.Entities.Text
{
    /// <inheritdoc cref="EntityBuffer" />
    /// <inheritdoc cref="IText"/>
    /// <summary>
    /// The <see cref="T:Dxflib.Entities.Text.MText" /> Buffer Class
    /// </summary>
    public class MTextBuffer : EntityBuffer, IText
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public MTextBuffer()
        {
            EntityType = typeof(MText);
            PositionVertex = new Vertex(0,0);
            Height = 0.2;
            ReferenceRecWidth = 0;
            Contents = string.Empty;
            TextStyle = new Style() {Name = "STANDARD"};
            DrawDirection = DrawDirections.ByStyle;
            BackgroundFillSetting = BackgroundFillSettings.Off;
            WidthFactor = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <inheritdoc />
        /// <summary>
        /// The Contents of the text
        /// </summary>
        public string Contents { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The Text Style
        /// </summary>
        public Style TextStyle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAnnotative { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Also known as the attachment point
        /// </summary>
        public JustifyOptions Justify { get; set; }

        /// <summary>
        /// <see cref="DrawDirections"/> setting
        /// </summary>
        public DrawDirections DrawDirection { get; set; }

        /// <summary>
        /// The Background fill setting
        /// </summary>
        public BackgroundFillSettings BackgroundFillSetting { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The Height of the MText
        /// </summary>
        public double Height { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Rotation in Radians
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
        /// The Width of the reference rectangle
        /// </summary>
        public double ReferenceRecWidth { get; set; }

        /// <summary>
        /// The Parsing function of the MText Object
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool Parse(TaggedDataList list, int index)
        {
            for ( var currentIndex = index + 1; currentIndex < list.Length; ++currentIndex )
            {
                var currentData = list.GetPair(currentIndex);

                if (currentData.GroupCode == GroupCodesBase.EntityType)
                    break;

                if (base.Parse(list, currentIndex))
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
