// Dxflib
// LwPolyLineBuffer.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-07-10:54 AM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using Dxflib.Geometry;
using Dxflib.Parser;

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
        ///     for the Lwpolyline class before it is built
        /// </summary>
        public LwPolyLineBuffer()
        {
            LayerName = "";
            Handle = "";
            NumberOfVerticies = 0;
            PolyLineFlag = false;
            ConstantWidth = 0;
            Elevation = 0;
            Thickness = 0;
            XValues = new List<double>();
            YValues = new List<double>();
            BulgeList = new List<double>();
        }

        /// <summary>
        ///     The Number of Verticies in the Polyline
        /// </summary>
        public int NumberOfVerticies { get; private set; }

        /// <summary>
        ///     The Polyline Flag, tells if the polyline is open or closed
        /// </summary>
        public bool PolyLineFlag { get; private set; }

        /// <summary>
        ///     Constant width or Global width
        /// </summary>
        public double ConstantWidth { get; private set; }

        /// <summary>
        ///     The elevation of the Polyline
        /// </summary>
        public double Elevation { get; private set; }

        /// <summary>
        ///     The Thickness of the polyline
        /// </summary>
        public double Thickness { get; private set; }

        /// <summary>
        ///     The Xvalues list
        /// </summary>
        public List<double> XValues { get; }

        /// <summary>
        ///     The YValues List
        /// </summary>
        public List<double> YValues { get; }

        /// <summary>
        ///     The bulge List
        /// </summary>
        public List<double> BulgeList { get; }

        /// <summary>
        ///     Main Parse Function for the Lwpolyline Class
        /// </summary>
        /// <param name="args">LineChangeHandlerArguments</param>
        /// <returns>True or false if the parse was sucessful</returns>
        public override bool Parse(LineChangeHandlerArgs args)
        {
            if (base.Parse(args))
                return true;

            switch (args.NewCurrentLine)
            {
                // Number of Verticies
                case LwPolyLineGroupGroupCodes.NumberOfVerticies:
                    NumberOfVerticies = int.Parse(args.NewNextLine);
                    return true;

                // Polyline Flag
                case LwPolyLineGroupGroupCodes.PolyLineFlag:
                    if (int.Parse(args.NewNextLine) == 0)
                        PolyLineFlag = false;
                    else if (int.Parse(args.NewNextLine) == 1)
                        PolyLineFlag = true;
                    return true;

                // Constant Width
                case LwPolyLineGroupGroupCodes.ConstantWidth:
                    ConstantWidth = double.Parse(args.NewNextLine);
                    return true;

                // Elevation
                case LwPolyLineGroupGroupCodes.Elevation:
                    Elevation = double.Parse(args.NewNextLine);
                    return true;

                // Thickness
                case LwPolyLineGroupGroupCodes.Thickness:
                    Thickness = double.Parse(args.NewNextLine);
                    return true;

                // Xvalues
                case LwPolyLineGroupGroupCodes.XValue:
                    BulgeList.Add(Bulge.BulgeNull);
                    XValues.Add(double.Parse(args.NewNextLine));
                    return true;

                // Yvalues
                case LwPolyLineGroupGroupCodes.YValue:
                    YValues.Add(double.Parse(args.NewNextLine));
                    return true;

                // Bulge Values
                case LwPolyLineGroupGroupCodes.Bulge:
                    BulgeList.RemoveAt(BulgeList.Count - 1);
                    BulgeList.Add(double.Parse(args.NewNextLine));
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     The LwPolyline Group codes
        /// </summary>
        private static class LwPolyLineGroupGroupCodes
        {
            public const string NumberOfVerticies = " 90";
            public const string PolyLineFlag = " 70";
            public const string ConstantWidth = " 43";
            public const string Elevation = " 38";
            public const string Thickness = " 39";
            public const string XValue = " 10";
            public const string YValue = " 20";
            public const string Bulge = " 42";
        }
    }
}