namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    ///     A class that contains all of the file variable codes
    ///     Note that these are not really the group codes but the
    ///     name of the variable. This was done to reduce the amount
    ///     of duplication in group codes
    /// </summary>
    public abstract class FileVariableCodes : GroupCodesBase
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
}