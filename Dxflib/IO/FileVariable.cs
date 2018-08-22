using System;

namespace Dxflib.IO
{
    public class FileVariable<T>
    {

        public FileVariable(string variableName, string value)
        {
            VariableName = variableName;
        }

        /// <summary>
        /// </summary>
        public string VariableName { get; }

        /// <summary>
        /// </summary>
        public virtual T Value { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StringVar : FileVariable<String>
    {
        public StringVar(string variableName, string value) : base(variableName, value)
        {
            // Set the value
            Value = value;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AutoCadVersionVar : FileVariable<AutoCadVersions>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        public AutoCadVersionVar(string value) : base(FileVariableCodes.AutoCadVersion, value)
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

