using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupCodesBase
    {
        // Basic
        public const string EntityType = "  0";
        public const string LayerName = "  8";
        public const string Handle = "  5";
        public const string SoftPointer = "330";
        public const string HardPointer = "360";
        public const string SubclassMarker = "100";

        // X Point
        public const string XPoint = " 10";
        public const string XPointEnd = " 11";

        // Y Point
        public const string YPoint = " 20";
        public const string YPointEnd = " 21";

        // Z Point
        public const string ZPoint = " 30";
        public const string ZPointEnd = " 31";

        // Extrusion Direction
        public const string ExtrusionDirectionX = "210";
        public const string ExtrusionDirectionY = "220";
        public const string ExtrusionDirectionZ = "230";

        // EndMarkers
        public const string EndSecionMarker = "ENDSEC";
    }

    public class FileVariableCodes : GroupCodesBase
    {
        public const string VariableNameIdentifier = "  9";
        public const string AutoCadVersion = "$ACADVER";
        public const string LastSavedBy = "$LASTSAVEDBY";
        public const string CurrentLayer = "$CLAYER";
    }

    /// <summary>
    /// 
    /// </summary>
    public class LineGroupCodes : GroupCodesBase
    {
        public const string StartMarker = "LINE";
        public const string Thickness = " 39";
    }

    /// <summary>
    /// 
    /// </summary>
    public class LwPolylineCodes : GroupCodesBase
    {
        public const string StartMarker = "LWPOLYLINE";
        public const string NumberOfVerticies = " 90";
        public const string PolylineFlag = " 70";
        public const string ConstantWidth = " 43";
        public const string Elevation = " 38";
        public const string Thickness = " 39";
        public const string VertexIdentifier = " 91";
        public const string StartingWidth = " 40";
        public const string EndingWith = " 41";
        public const string Bulge = " 42";
    }
}
