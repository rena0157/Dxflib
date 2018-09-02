namespace Dxflib.IO.GroupCodes
{
    /// <summary>
    ///     The base class for group codes.
    /// </summary>
    /// <remarks>
    ///     The group codes in this section
    ///     are as referenced in the "Dxf Group Codes in Numerical Order"
    ///     Section of the 2019 Dxf reference.
    /// </remarks>
    public abstract class GroupCodesBase
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
        /// The Thickness of the Entity
        /// </summary>
        public const string Thickness = " 39";


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
}