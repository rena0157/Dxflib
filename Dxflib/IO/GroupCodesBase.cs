// Dxflib
// GroupCodesBase.cs
// 
// ============================================================
// 
// Created: 2018-08-21
// Last Updated: 2018-08-23-9:18 PM
// By: Adam Renaud
// 
// ============================================================

// ReSharper disable ClassNeverInstantiated.Global
namespace Dxflib.IO
{
    /// <summary>
    ///     The base class for group codes.
    /// </summary>
    /// <remarks>
    ///     The group codes in this section
    ///     are as referenced in the "Dxf Group Codes in Numerical Order"
    ///     Section of the 2019 Dxf reference.
    /// </remarks>
    public class GroupCodesBase
    {
        /// <summary>
        ///     Text string indicating the entity type (fixed)
        /// </summary>
        public const string EntityType = "  0";

        /// <summary>
        ///     Layer name (fixed)
        /// </summary>
        public const string LayerName = "  8";

        /// <summary>
        ///     Entity handle; text string of up to 16 hexadecimal digits (fixed)
        /// </summary>
        public const string Handle = "  5";

        /// <summary>
        ///     Soft-pointer handle; arbitrary soft pointers to other objects within
        ///     same DXF file or drawing. Translated during INSERT and XREF operations
        /// </summary>
        public const string SoftPointer = "330";

        /// <summary>
        ///     Hard-owner handle; arbitrary hard ownership links to other objects within
        ///     same DXF file or drawing. Translated during INSERT and XREF operations
        /// </summary>
        public const string HardPointer = "360";

        /// <summary>
        ///     Subclass data marker (with derived class name as a string).
        ///     Required for all objects and entity classes that are derived from another concrete class
        /// </summary>
        public const string SubclassMarker = "100";


        /// <summary>
        ///     DXF: X value of the primary point (followed by Y and Z value codes 20 and 30)
        /// </summary>
        public const string XPoint = " 10";

        /// <summary>
        ///     DXF: X value of other points (followed by Y value codes 21-28 and Z value codes 31-38)
        /// </summary>
        public const string XPointEnd = " 11";


        // Y Point
        /// <summary>
        ///     <see cref="XPoint" />
        /// </summary>
        public const string YPoint = " 20";

        /// <summary>
        ///     <see cref="XPointEnd" />
        /// </summary>
        public const string YPointEnd = " 21";


        // Z Point
        /// <summary>
        ///     <see cref="XPoint" />
        /// </summary>
        public const string ZPoint = " 30";

        /// <summary>
        ///     <see cref="XPointEnd" />
        /// </summary>
        public const string ZPointEnd = " 31";

        /// <summary>
        ///     DXF: X value of extrusion direction
        /// </summary>
        public const string ExtrusionDirectionX = "210";

        /// <summary>
        ///     DXF: Y value of extrusion direction
        /// </summary>
        public const string ExtrusionDirectionY = "220";

        /// <summary>
        ///     DXF: X value of extrusion direction
        /// </summary>
        public const string ExtrusionDirectionZ = "230";

        /// <summary>
        ///     The End section Marker
        /// </summary>
        public const string EndSectionMarker = "ENDSEC";
    }

    /// <inheritdoc />
    /// <summary>
    ///     A class that contains all of the file variable codes
    ///     Note that these are not really the group codes but the
    ///     name of the variable. This was done to reduce the amount
    ///     of duplication in group codes
    /// </summary>
    public class FileVariableCodes : GroupCodesBase
    {
        /// <summary>
        ///     The Variable Name Identifier
        /// </summary>
        public const string VariableNameIdentifier = "  9";

        /// <summary>
        ///     The AutoCAD Version
        /// </summary>
        public const string AutoCadVersion = "$ACADVER";

        /// <summary>
        ///     Who the file was last saved by
        /// </summary>
        public const string LastSavedBy = "$LASTSAVEDBY";

        /// <summary>
        ///     The Current Layer when the drawing was last saved
        /// </summary>
        public const string CurrentLayer = "$CLAYER";
    }

    /// <inheritdoc />
    /// <summary>
    ///     The <see cref="T:Dxflib.Entities.Line" /> entity group codes
    /// </summary>
    public class LineGroupCodes : GroupCodesBase
    {
        /// <summary>
        ///     The starting marker of the line
        /// </summary>
        public const string StartMarker = "LINE";

        /// <summary>
        ///     The thickness of the line
        /// </summary>
        public const string Thickness = " 39";
    }

    /// <inheritdoc />
    /// <summary>
    ///     The <see cref="T:Dxflib.Entities.LwPolyLine" /> entity group codes
    /// </summary>
    public class LwPolylineCodes : GroupCodesBase
    {
        /// <summary>
        ///     The Starting marker of the entity
        /// </summary>
        public const string StartMarker = "LWPOLYLINE";

        /// <summary>
        ///     Number of vertices
        /// </summary>
        public const string NumberOfVertices = " 90";

        /// <summary>
        ///     Polyline flag (bit-coded); default is 0: 1 = Closed
        /// </summary>
        public const string PolylineFlag = " 70";

        /// <summary>
        ///     Constant width (optional; default = 0).
        ///     Not used if variable width (codes 40 and/or 41) is set
        /// </summary>
        public const string ConstantWidth = " 43";

        /// <summary>
        ///     Elevation (optional; default = 0)
        /// </summary>
        public const string Elevation = " 38";

        /// <summary>
        ///     Thickness (optional; default = 0)
        /// </summary>
        public const string Thickness = " 39";

        /// <summary>
        ///     The Vertex Identifier
        /// </summary>
        public const string VertexIdentifier = " 91";

        /// <summary>
        ///     Starting width (multiple entries; one entry for each vertex)
        ///     (optional; default = 0; multiple entries). Not used if constant width (code 43) is set
        /// </summary>
        public const string StartingWidth = " 40";

        /// <summary>
        ///     End width (multiple entries; one entry for each vertex)
        ///     (optional; default = 0; multiple entries). Not used if constant width (code 43) is set
        /// </summary>
        public const string EndingWith = " 41";

        /// <summary>
        ///     Bulge (multiple entries; one entry for each vertex) (optional; default = 0)
        /// </summary>
        public const string Bulge = " 42";
    }

    /// <inheritdoc />
    /// <summary>
    /// The HatchGroupCodes
    /// </summary>
    public class HatchCodes : GroupCodesBase
    {
        /// <summary>
        /// The Start Marker of the Hatch Entity
        /// </summary>
        public const string StartMarker = "HATCH";

        /// <summary>
        /// Hatch pattern name
        /// </summary>
        public const string HatchPatternName = "  2";

        /// <summary>
        /// Solid fill flag (0 = pattern fill; 1 = solid fill);
        /// for MPolygon, the version of MPolygon
        /// </summary>
        public const string SolidFillFlag = " 70";

        /// <summary>
        /// Associativity flag (0 = non-associative; 1 = associative);
        /// for MPolygon, solid-fill flag (0 = lacks solid fill; 1 = has solid fill)
        /// </summary>
        public const string AssociativityFlag = " 71";

        /// <summary>
        /// Number of boundary paths (loops)
        /// </summary>
        public const string NumberOfBoundaryLoops = " 91";

        /// <summary>
        ///Hatch style:
        ///0 = Hatch “odd parity” area (Normal style)
        ///1 = Hatch outermost area only (Outer style)
        ///2 = Hatch through entire area (Ignore style)
        /// </summary>
        public const string HatchStyle = " 75";

        /// <summary>
        /// Hatch Pattern type
        /// 0 = User defined
        /// 1 = Predefined
        /// 2 = Custom
        /// </summary>
        public const string HatchPatternType = " 76";

        /// <summary>
        /// Hatch Pattern Angle (pattern fill only)
        /// </summary>
        public const string HatchPatternAngle = " 52";

        /// <summary>
        /// Hatch Pattern Scale or spacing (pattern fill only)
        /// </summary>
        public const string HatchPatternScale = " 41";

        /// <summary>
        /// Number of Pattern Definition Lines
        /// </summary>
        public const string NumberOfPatternDefLines = " 78";
    }
}