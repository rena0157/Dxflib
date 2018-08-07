// Dxflib
// LwPolyLine.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-08-07-8:04 AM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using Dxflib.Geometry;
using Dxflib.Parser;

namespace Dxflib.Entities
{
    public class LwPolyLine : Entity
    {
        // The Geometric backing class
        private readonly GeoPolyline _geoPolyline;

        public LwPolyLine(LwPolyLineBuffer lwPolyLineBuffer)
        {
            // Entity Base
            EntityType = EntityTypes.Lwpolyline;
            LayerName = lwPolyLineBuffer.LayerName;
            Handle = lwPolyLineBuffer.Handle;

            // LwPolyLine Specific
            NumberOfVerticies = lwPolyLineBuffer.NumberOfVerticies;
            PolyLineFlag = lwPolyLineBuffer.PolyLineFlag;
            ConstantWidth = lwPolyLineBuffer.ConstantWidth;
            Elevation = lwPolyLineBuffer.Elevation;
            Thickness = lwPolyLineBuffer.Thickness;

            // Create the GeoPolyline
            _geoPolyline = new GeoPolyline(
                lwPolyLineBuffer.XValues, lwPolyLineBuffer.YValues,
                lwPolyLineBuffer.BulgeList, PolyLineFlag);
        }

        // AutoCAD Entity Properties
        public int NumberOfVerticies { get; }
        public bool PolyLineFlag { get; }
        public double ConstantWidth { get; }
        public double Elevation { get; }
        public double Thickness { get; }

        // Geometric Properties
        public double Length => _geoPolyline.Length;
        public double Area { get; }

    }

    /// <inheritdoc />
    /// <summary>
    ///     The Buffer class for holding lwpolyling information during the extraction process
    /// </summary>
    public class LwPolyLineBuffer : EntityBuffer
    {
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

        public int NumberOfVerticies { get; private set; }
        public bool PolyLineFlag { get; private set; }
        public double ConstantWidth { get; set; }
        public double Elevation { get; set; }
        public double Thickness { get; set; }
        public List<double> XValues { get; set; }
        public List<double> YValues { get; set; }
        public List<double> BulgeList { get; set; }

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
                    BulgeList.RemoveAt(BulgeList.Count -1);
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