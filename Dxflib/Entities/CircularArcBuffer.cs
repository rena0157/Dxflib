// Dxflib
// CircularArcBuffer.cs
// 
// ============================================================
// 
// Created: 2018-09-01
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.IO;
using Dxflib.IO.GroupCodes;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     The a child of the <see cref="T:Dxflib.Entities.EntityBuffer" /> class that
    ///     is used to create <see cref="T:Dxflib.Entities.CircularArc" /> Entities
    /// </summary>
    public class CircularArcBuffer : EntityBuffer
    {
        /// <inheritdoc />
        /// <summary>
        ///     Constructor for the Circular Arc Buffer Class
        ///     This constructor will set all properties to their default values
        /// </summary>
        public CircularArcBuffer()
        {
            EntityType = typeof(CircularArc);
            Thickness = 0.0;
            CenterPointX = 0.0;
            CenterPointY = 0.0;
            CenterPointZ = 0.0;
            Radius = 0.0;
            StartAngle = 0.0;
            EndAngle = 0.0;
        }

        /// <summary>
        ///     <see cref="CircularArcCodes.Thickness" />
        /// </summary>
        public double Thickness { get; private set; }

        /// <summary>
        ///     The Center Point X position
        /// </summary>
        public double CenterPointX { get; private set; }

        /// <summary>
        ///     The Center Point Y Position
        /// </summary>
        public double CenterPointY { get; private set; }

        /// <summary>
        ///     The Center Point Z Position
        /// </summary>
        public double CenterPointZ { get; private set; }

        /// <summary>
        ///     The Radius of the Arc
        /// </summary>
        public double Radius { get; private set; }

        /// <summary>
        ///     The Start Angle of the Arc in degrees
        /// </summary>
        public double StartAngle { get; private set; }

        /// <summary>
        ///     The End Angle of the Arc in degrees
        /// </summary>
        public double EndAngle { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Parse Function for the CircularArc Entity
        /// </summary>
        /// <param name="list">The Tagged Data list</param>
        /// <param name="index">The Current Index</param>
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
                        CenterPointX = double.Parse(currentData.Value);
                        continue;

                    case GroupCodesBase.YPoint:
                        CenterPointY = double.Parse(currentData.Value);
                        continue;

                    case GroupCodesBase.ZPoint:
                        CenterPointZ = double.Parse(currentData.Value);
                        continue;

                    case CircularArcCodes.Thickness:
                        Thickness = double.Parse(currentData.Value);
                        continue;

                    case CircularArcCodes.Radius:
                        Radius = double.Parse(currentData.Value);
                        continue;

                    case CircularArcCodes.StartAngle:
                        StartAngle = double.Parse(currentData.Value);
                        continue;

                    case CircularArcCodes.EndAngle:
                        EndAngle = double.Parse(currentData.Value);
                        continue;
                    default:
                        continue;
                }
            }

            return true;
        }
    }
}