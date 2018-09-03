using System;
using System.Collections.Generic;
using System.Text;
using Dxflib.Geometry;
using Dxflib.IO;
using Dxflib.IO.GroupCodes;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Buffer Class for the <see cref="T:Dxflib.Entities.Point" /> Entity
    /// </summary>
    public class PointBuffer : EntityBuffer
    {
        /// <inheritdoc />
        /// <summary>
        /// Default buffer constructor that will set all properties to defaults
        /// </summary>
        public PointBuffer()
        {
            EntityType = typeof(Point);
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
        }

        /// <summary>
        /// X position
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y Position
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Z Position
        /// </summary>
        public double Z { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The Parse Function for the <see cref="T:Dxflib.Entities.Point" /> Entity
        /// </summary>
        /// <param name="list">The Tagged data list of group codes and values</param>
        /// <param name="index">The current Index of the parser</param>
        /// <returns>Always True</returns>
        public override bool Parse(TaggedDataList list, int index)
        {
            for ( var currentIndex = index + 1; currentIndex < list.Length; ++currentIndex )
            {
                var currentData = list.GetPair(currentIndex);

                if ( currentData.GroupCode == GroupCodesBase.EntityType )
                    break;

                if (base.Parse(list, currentIndex))
                    continue;

                switch ( currentData.GroupCode )
                {
                    case GroupCodesBase.XPoint:
                        X = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.YPoint:
                        Y = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.ZPoint:
                        Z = double.Parse(currentData.Value);
                        continue;
                    case PointCodes.AngleOfXAxis:
                        continue;
                    default:
                        continue;
                }
            }

            return true;
        }
    }
}
