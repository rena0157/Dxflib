namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    ///     The <see cref="T:Dxflib.Entities.Line" /> entity group codes
    /// </summary>
    public abstract class LineGroupCodes : GroupCodesBase
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
}