// Dxflib
// FileVariable.cs
// 
// ============================================================
// 
// Created: 2018-08-21
// Last Updated: 2018-08-23-8:56 PM
// By: Adam Renaud
// 
// ============================================================

namespace Dxflib.IO
{
    /// <summary>
    ///     A generic class that is used to act as a base for
    ///     other file variable classes to specialize and inherit from
    /// </summary>
    /// <typeparam name="T">The Typename of the file variable</typeparam>
    public class FileVariable<T>
    {
        /// <summary>
        ///     The main constructor for the FileVariable Class.
        ///     This Constructor will set the variable name.
        /// </summary>
        /// <param name="variableName"></param>
        protected FileVariable(string variableName) { VariableName = variableName; }

        /// <summary>
        ///     The Variable Name
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string VariableName { get; }

        /// <summary>
        ///     The Variables Value
        /// </summary>
        public virtual T Value { get; set; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     The String Variable Class for AutoCAD File Variables.
    ///     This class is a specialization of the <see cref="T:Dxflib.IO.FileVariable`1" />
    ///     class where T is a <see cref="T:System.String" />.
    /// </summary>
    public sealed class StringVar : FileVariable<string>
    {
        /// <inheritdoc />
        /// <summary>
        ///     The main constructor for a string variable
        /// </summary>
        /// <param name="variableName">
        ///     The file variable name eg. "$LASTSAVEDBY"
        ///     The last saved by file variable
        /// </param>
        /// <param name="value">The value of the variable</param>
        public StringVar(string variableName, string value) : base(variableName)
        {
            // Set the value
            Value = value;
        }
    }

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