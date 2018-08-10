// Dxflib
// HeaderParser.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-10-1:05 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;
using Dxflib.AcadEntities;

namespace Dxflib.Parser
{
    /// <summary>
    ///     Static class that holds all of the functionality for parsing the header section of the Dxf File
    /// </summary>
    public static class HeaderParser
    {
        /// <summary>
        ///     Main Parse Function that will set values in the attached main parsers dxf file
        /// </summary>
        /// <param name="mainParser">The main calling parser</param>
        /// <param name="args">The Line changed event arguments</param>
        public static void Parse(DxfFileMainParser mainParser, LineChangeHandlerArgs args)
        {
            switch ( args.NewCurrentLine )
            {
                // AutoCAD Version
                case HeaderStrings.AutoCadVersion:
                    mainParser.ThisFile.AutoCADVersion = ParseAutoCadVersion(
                        mainParser.ThisFile.ContentStrings[args.LineIndex + 2]);
                    break;
                // Current Layer
                case HeaderStrings.CurrentLayer:
                    mainParser.ThisFile.CurrentLayer =
                        new Layer(mainParser.ThisFile.ContentStrings[args.LineIndex + 2]);
                    break;
                // Last Saved By
                case HeaderStrings.LastSavedBy:
                    mainParser.ThisFile.LastSavedBy =
                        mainParser.ThisFile.ContentStrings[args.LineIndex + 2];
                    break;
            }
        }

        /// <summary>
        ///     This function converts strings to the AutoCADVersions enum
        /// </summary>
        /// <param name="line">The string that is to be parsed</param>
        /// <returns>A corresponding AutoCADVersion</returns>
        private static AutoCadVersions ParseAutoCadVersion(string line)
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

    /// <summary>
    ///     Main header strings that are used in the main parse function for
    ///     headers. This class is just a selection of strings
    /// </summary>
    public static class HeaderStrings
    {
        public const string AutoCadVersion = "$ACADVER";
        public const string CurrentLayer = "$CLAYER";
        public const string LastSavedBy = "$LASTSAVEDBY";
    }

    /// <summary>
    ///     AutoCAD Versions up to 2013
    /// </summary>
    public enum AutoCadVersions
    {
        [Description("R10")] AC1006,
        [Description("R11 and R12")] AC1009,
        [Description("R13")] AC1012,
        [Description("R14")] AC1014,
        [Description("AutoCAD 2000")] AC1015,
        [Description("AutoCAD 2004")] AC1018,
        [Description("AutoCAD 2007")] AC1021,
        [Description("AutoCAD 2010")] AC1024,
        [Description("AutoCAD 2013")] AC1027,
        [Description("Unknown")] Unknown
    }
}