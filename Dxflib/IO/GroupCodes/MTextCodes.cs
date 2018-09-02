using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Dxflib.IO.GroupCodes
{
    /// <inheritdoc />
    /// <summary>
    /// Group Codes for the <see cref="Dxflib.Entities.Text.MText"/> Entity
    /// </summary>
    public abstract class MTextCodes : TextCodes
    {
        /// <summary>
        /// Reference Rectangle Width
        /// </summary>
        public const string ReferenceRectangleWidth = " 41";

        /// <summary>
        /// Attachment Point
        /// </summary>
        /// <remarks>
        /// 1 = Top Left
        /// 2 = Top Center
        /// 3 = Top Right
        /// 4 = Middle Left
        /// 5 = Middle Center
        /// 6 = Middle Right
        /// 7 = Bottom Left
        /// 8 = Bottom Center
        /// 9 = Bottom Right
        /// </remarks>
        public const string AttachmentPoint = " 71";

        /// <summary>
        /// Draw Direction
        /// </summary>
        /// <remarks>
        /// 1 = Left to right
        /// 3 = Top to bottom
        /// 5 = By style (the flow direction is inherited from the associated text style) 
        /// </remarks>
        public const string DrawDirection = " 72";

        /// <summary>
        /// Additional Text - Always in 250 char chunks, optional
        /// </summary>
        public const string AdditionalText = "  3";

        /// <summary>
        /// Horizontal Width of the chars that make up the <see cref="Dxflib.Entities.Text.MText"/> Entity
        /// </summary>
        /// <remarks>
        /// This value will always be equal to or less than the value of group code <see cref="ReferenceRectangleWidth"/>
        /// this value is read only and will be ignored if supplied
        /// </remarks>
        public const string HorizontalWidth = " 42";

        /// <summary>
        /// Vertical Height of the <see cref="Dxflib.Entities.Text.MText"/> entity
        /// </summary>
        public const string VerticalHeight = " 43";

        /// <summary>
        /// <see cref="Dxflib.Entities.Text.MText"/> line spacing style (optional)
        /// </summary>
        /// <remarks>
        /// 1 = At least (taller chars will override)
        /// 2 = Exact (taller chars will not override)
        /// </remarks>
        public const string LineSpacingStyle = " 73";

        /// <summary>
        /// <see cref="Dxflib.Entities.Text.MText"/> line spacing factor (optional)
        /// </summary>
        /// <remarks>
        /// Percentage of default (3-on-5) line spacing to be applied. Valid values range from
        /// 0.25 to 4.00
        /// </remarks>
        public const string LineSpacingFactor = " 44";

        /// <summary>
        /// Background Fill Setting
        /// </summary>
        /// <remarks>
        /// 0 = Background Fill off
        /// 1 = Use Background Color
        /// 2 = Use Drawing window color as background fill
        /// </remarks>
        public const string BackgroundFillSetting = " 90";

        /// <summary>
        /// Background Color (RGB)
        /// </summary>
        public const string BackgroundColorRgb = "420";

        /// <summary>
        /// Background color (Name)
        /// </summary>
        public const string BackgroundColorName = "430";

        /// <summary>
        /// Fill Box Scale
        /// </summary>
        /// <remarks>
        /// Determines how much border there is around the text
        /// </remarks>
        public const string FillBoxScale = " 45";

        /// <summary>
        /// Background Fill Color, color to be used if <see cref="BackgroundFillSetting"/> = 1
        /// </summary>
        public const string BackgroundFillColor = " 63";

        /// <summary>
        /// Transparency of the background fill color (not implemented)
        /// </summary>
        public const string TransparencyOfBackgroundFill = "441";

    }
}
