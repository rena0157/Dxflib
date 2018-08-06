using System.Collections.Generic;
using System.Net.Http.Headers;
using Dxflib.Geometry;
using Dxflib.Parser;

namespace Dxflib.Entities
{
    public class LwPolyLine : Entity
    {
        public LwPolyLine(LwPolyLineBuffer lwPolyLineBuffer)
        {
            
        }

        private List<GeometricEntityBase> _sections;

        private List<GeometricEntityBase> SectionBuilder()
        {
            return new List<GeometricEntityBase>();
        }
    }

    public class LwPolyLineBuffer : EntityBuffer
    {

        public LwPolyLineBuffer()
        {
            NumberOfVerticies = 0;
            PolyLineFlag = false;
            ConstantWidth = 0;
            Elevation = 0;
            Thickness = 0;
            XValues = new List<double>();
            YValues = new List<double>();
            BulgeList = new List<double>();
        }

        public int NumberOfVerticies { get; set; }
        public bool PolyLineFlag { get; set; }
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
                    XValues.Add(double.Parse(args.NewNextLine));
                    return true;
                
                // Yvalues
                case LwPolyLineGroupGroupCodes.YValue:
                    YValues.Add(double.Parse(args.NewNextLine));
                    return true;
                
                // Bulge Values
                case LwPolyLineGroupGroupCodes.Bulge:
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
            public const string Thickness = "  39";
            public const string XValue = " 10";
            public const string YValue = " 20";
            public const string Bulge = " 42";
        }
    }
}