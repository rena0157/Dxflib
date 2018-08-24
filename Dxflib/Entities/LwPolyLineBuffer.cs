// Dxflib
// LwPolyLineBuffer.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-12-2:24 PM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using Dxflib.Geometry;
using Dxflib.IO;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     The Buffer class for holding lwpolyling information during the extraction process
    /// </summary>
    public class LwPolyLineBuffer : EntityBuffer
    {
        /// <inheritdoc />
        /// <summary>
        ///     The Lwpolyline Buffer Constructor that holds all information
        ///     for the Lwpolyline class before it is built.
        /// </summary>
        public LwPolyLineBuffer()
        {
            LayerName = "";
            Handle = "";
            NumberOfVertices = 0;
            PolyLineFlag = false;
            ConstantWidth = 0;
            Elevation = 0;
            Thickness = 0;
            XValues = new List<double>();
            YValues = new List<double>();
            BulgeList = new List<double>();
        }

        /// <summary>
        ///     The Number of Vertices in the Lwpolyline
        /// </summary>
        public int NumberOfVertices { get; private set; }

        /// <summary>
        ///     The Lwpolyline Flag, tells if the lwpolyline is open or closed
        /// </summary>
        public bool PolyLineFlag { get; private set; }

        /// <summary>
        ///     Constant width or Global width
        /// </summary>
        public double ConstantWidth { get; private set; }

        /// <summary>
        ///     The elevation of the Lwpolyline
        /// </summary>
        public double Elevation { get; private set; }

        /// <summary>
        ///     The Thickness of the lwpolyline
        /// </summary>
        public double Thickness { get; private set; }

        /// <summary>
        ///     The X values list
        /// </summary>
        public List<double> XValues { get; }

        /// <summary>
        ///     The Y Values List
        /// </summary>
        public List<double> YValues { get; }

        /// <summary>
        ///     The bulge List
        /// </summary>
        public List<double> BulgeList { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Main Parse Function for the Lwpolyline Class
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>True or false if the parse was successful</returns>
        public override bool Parse(TaggedDataList list, int index)
        {
            EntityType = EntityTypes.Lwpolyline;
            for ( var currentIndex = index + 1;
                currentIndex < list.Length;
                ++currentIndex )
            {
                var currentData = list.GetPair(currentIndex);

                if (currentData.GroupCode == GroupCodesBase.EntityType)
                    break;

                if (base.Parse(list, currentIndex))
                    continue;

                switch ( currentData.GroupCode )
                {
                    // Number of Vertices
                    case LwPolylineCodes.NumberOfVertices:
                        NumberOfVertices = int.Parse(currentData.Value);
                        continue;  

                    // Lwpolyline Flag
                    case LwPolylineCodes.PolylineFlag:
                        PolyLineFlag = currentData.Value.Contains("1");
                        continue;

                    // Constant Width
                    case LwPolylineCodes.ConstantWidth:
                        ConstantWidth = double.Parse(currentData.Value);
                        continue;

                    // Elevation
                    case LwPolylineCodes.Elevation:
                        Elevation = double.Parse(currentData.Value);
                        continue;
                    
                    // Thickness
                    case LwPolylineCodes.Thickness:
                        Thickness = double.Parse(currentData.Value);
                        continue;

                    // X values
                    case GroupCodesBase.XPoint:
                        BulgeList.Add(Bulge.BulgeNull);
                        XValues.Add(double.Parse(currentData.Value));
                        continue;

                    // Y values
                    case GroupCodesBase.YPoint:
                        YValues.Add(double.Parse(currentData.Value));
                        continue;

                    // Bulge Values
                    case LwPolylineCodes.Bulge:
                        BulgeList.RemoveAt(BulgeList.Count - 1);
                        BulgeList.Add(double.Parse(currentData.Value));
                        continue;
                    default:
                        continue;
                }
            }
            return true;
        }
    }
}