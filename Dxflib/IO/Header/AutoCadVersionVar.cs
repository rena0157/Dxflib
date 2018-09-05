// Dxflib
// FileVariable.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.IO.GroupCodes;

namespace Dxflib.IO.Header
{
    /// <inheritdoc />
    /// <summary>
    ///     The AutoCADVersion Variable class. This class is a
    ///     specialization of the <see cref="FileVariable{T}" /> class
    ///     where T is an <see cref="AutoCadVersions" />. This class is used
    ///     for the AutoCAD Version in the dxf file.
    /// </summary>
    public sealed class AutoCadVersionVar : FileVariable<AutoCadVersions>
    {
        /// <inheritdoc />
        /// <summary>
        ///     The main constructor which will set the value of the variable.
        /// </summary>
        /// <param name="value"></param>
        public AutoCadVersionVar(string value) : base(FileVariableCodes.AutoCadVersion)
        {
            Value = ParseAutoCadVersion(value);
        }

        /// <summary>
        ///     This function converts strings to the AutoCADVersions enum
        /// </summary>
        /// <param name="line">The string that is to be parsed</param>
        /// <returns>A corresponding AutoCADVersion</returns>
        public static AutoCadVersions ParseAutoCadVersion(string line)
        {
            switch ( line )
            {
                case "AC1006": return AutoCadVersions.AC1006;
                case "AC1009": return AutoCadVersions.AC1009;
                case "AC1012": return AutoCadVersions.AC1012;
                case "AC1014": return AutoCadVersions.AC1014;
                case "AC1015": return AutoCadVersions.AC1015;
                case "AC1018": return AutoCadVersions.AC1018;
                case "AC1021": return AutoCadVersions.AC1021;
                case "AC1024": return AutoCadVersions.AC1024;
                case "AC1027": return AutoCadVersions.AC1027;
                default: return AutoCadVersions.Unknown;
            }
        }
    }
}