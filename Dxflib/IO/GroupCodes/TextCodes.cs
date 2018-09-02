// Dxflib
// TextCodes.cs
// 
// ============================================================
// 
// Created: 2018-09-02
// Last Updated: 2018-09-02-10:39 AM
// By: Adam Renaud
// 
// ============================================================

namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    ///     Group Codes for the <see cref="T:Dxflib.Entities.Text.Text" /> Entity
    /// </summary>
    public abstract class TextCodes : GroupCodesBase
    {
        /// <summary>
        /// The Start Marker of the <see cref="Dxflib.Entities.Text.Text"/> Entity
        /// </summary>
        public const string StartMarker = "TEXT";

        /// <summary>
        /// Text Height
        /// </summary>
        public const string TextHeight = " 40";

        /// <summary>
        /// The Text String Itself
        /// </summary>
        public const string TextString = "  1";

        /// <summary>
        /// Text Rotation (Optional; Default = 0)
        /// </summary>
        public const string TextRotation = " 50";

        /// <summary>
        /// Relative X Scale Factor (optional; default = 0)
        /// This value is also adjusted when fit-type text is used
        /// </summary>
        public const string RelativeXScale = " 41";

        /// <summary>
        /// The Oblique Angle of the text (Optional; Default = 0)
        /// </summary>
        public const string ObliqueAngle = " 51";

        /// <summary>
        /// Text Style Name (Optional, default = STANDARD)
        /// </summary>
        public const string TextStyleName = "  7";

        /// <summary>
        /// Text Generation Flag (optional, default = 0)
        /// 2 = Text is backwards (Mirrored in X)
        /// </summary>
        public const string TextGenerationFlag = " 71";

        /// <summary>
        /// Horizontal text justification type (optional, default = 0) integer codes (not bit codes)
        /// </summary>
        /// <remarks>
        /// 0 = Left
        /// 1 = Center
        /// 2 = Right
        /// 3 = Aligned (if vertical alignment = 0)
        /// 4 = Middle (if vertical alignment = 0)
        /// 5 = Fit (if vertical alignment = 0)
        /// See the group coded 72 and 73 table for clarification
        /// <see href="http://help.autodesk.com/view/OARX/2019/ENU/?guid=GUID-62E5383D-8A14-47B4-BFC4-35824CAE8363"/>
        /// </remarks>
        public const string HorizontalJustification = " 72";

        /// <summary>
        /// Vertical Text Justification (optional, default = 0) integer codes (not bit codes)
        /// </summary>
        /// <remarks>
        /// 0 = Baseline
        /// 1 = Bottom
        /// 2 = Middle
        /// 3 = Top
        /// See the Group codes 72 and 73 table for clarification
        /// <see href="http://help.autodesk.com/view/OARX/2019/ENU/?guid=GUID-62E5383D-8A14-47B4-BFC4-35824CAE8363"/>
        /// </remarks>
        public const string VerticalJustification = " 73";
    }
}