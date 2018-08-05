using Dxflib.Geometry;
using Dxflib.Parser;

namespace Dxflib.Entities
{
    public class Line : Entity
    {
        public Line(LineBuffer lineBuffer)
        {
            EntityType = EntityTypes.Line;
            Handle = lineBuffer.handle;
            LayerName = lineBuffer.LayerName;

            // Setting the GeoLine
            GLine = new GeoLine(new Vertex(lineBuffer.x0, lineBuffer.y0),
                new Vertex(lineBuffer.x1, lineBuffer.y1));
        }

        public GeoLine GLine { get; }
        public double Length => GLine.Length;
    }

    public class LineBuffer : EntityBuffer
    {
        public LineBuffer()
        {
            Thickness = 0;
            x0 = 0;
            x1 = 0;
            y0 = 0;
            y1 = 0;
        }

        public double Thickness { get; set; }
        public double x0 { get; set; }
        public double x1 { get; set; }
        public double y0 { get; set; }
        public double y1 { get; set; }

        public override bool Parse(LineChangeHandlerArgs args)
        {
            var currentLine = args.NewCurrentLine;
            var nextLine = args.NewNextLine;

            if (base.Parse(args))
                return true;

            switch (currentLine)
            {
                case LineGroupCodes.Thickness:
                    return true;
                case LineGroupCodes.x0:
                    x0 = double.Parse(args.NewNextLine);
                    return true;
                case LineGroupCodes.x1:
                    x1 = double.Parse(args.NewNextLine);
                    return true;
                case LineGroupCodes.y0:
                    y0 = double.Parse(args.NewNextLine);
                    return true;
                case LineGroupCodes.y1:
                    y1 = double.Parse(args.NewNextLine);
                    return true;
                default: return false;
            }
        }

        // Group Codes
    }

    public static class LineGroupCodes
    {
        public const string Thickness = "39";
        public const string x0 = " 10";
        public const string x1 = " 11";
        public const string y0 = " 20";
        public const string y1 = " 21";
    }
}