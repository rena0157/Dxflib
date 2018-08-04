

using System.ComponentModel;
using Dxflib.AcadEntities;

namespace Dxflib.Parser
{
    /// <summary>
    /// Static class that holds all of the functionality for parsing the header section of the Dxf File
    /// </summary>
    public static class HeaderParser
    {
        /// <summary>
        /// Main Parse Fucntion that will set values in the attached main parsers dxf file
        /// </summary>
        /// <param name="mainParser">The main calling parser</param>
        /// <param name="args">The Line changed event arguments</param>
        public static void Parse(DxfFileMainParser mainParser, LineChangeHandlerArgs args)
        {
            switch (args.NewCurrentLine)
            {
                case HeaderStrings.AutoCadVersion:
                    mainParser.ThisFile.AutoCADVersion = ParseAutoCADVersion(args.NewNextLine);
                    break;
                case HeaderStrings.CurrentLayer:
                    mainParser.ThisFile.CurrentLayer = new Layer(args.NewNextLine);
                     break;
                case HeaderStrings.LastSavedBy:
                     mainParser.ThisFile.LastSavedBy = args.NewNextLine;
                    break;
                default:
                   break;
            }
        }

        /// <summary>
        /// This function converts strings to the AutoCADVersions enum
        /// </summary>
        /// <param name="line">The string that is to be parsed</param>
        /// <returns>A coresponding AutoCADVersion</returns>
        public static AutoCADVersions ParseAutoCADVersion(string line)
        {
            switch (line)
            {
                case "AC1006": return AutoCADVersions.AC1006;
                case "AC1009": return AutoCADVersions.AC1009;
                case "AC1012": return AutoCADVersions.AC1012;
                case "AC1014": return AutoCADVersions.AC1014;
                case "AC1015": return AutoCADVersions.AC1015;
                case "AC1018": return AutoCADVersions.AC1018;
                case "AC1021": return AutoCADVersions.AC1021;
                case "AC1024": return AutoCADVersions.AC1024;
                case "AC1027": return AutoCADVersions.AC1027;
                default: return AutoCADVersions.Unknown;
            }
        }

    }

    /// <summary>
    /// Main header strings that are used in the main parse function for
    /// headers. This class is just a selection of strings
    /// </summary>
    public static class HeaderStrings
    {
        public const string AutoCadVersion = "$ACADVER";
        public const string CurrentLayer = "$CLAYER";
        public const string LastSavedBy = "$LASTSAVEDBY";

    }

    /// <summary>
    /// AutoCAD Versions up to 2013
    /// </summary>
    public enum AutoCADVersions
    {
        [Description("R10")]
        AC1006,

        [Description("R11 and R12")]
        AC1009,

        [Description("R13")]
        AC1012,

        [Description("R14")]
        AC1014,

        [Description("AutoCAD 2000")]
        AC1015,

        [Description("AutoCAD 2004")]
        AC1018,

        [Description("AutoCAD 2007")]
        AC1021,

        [Description("AutoCAD 2010")]
        AC1024,

        [Description("AutoCAD 2013")]
        AC1027,

        [Description("Unknown")]
        Unknown
    }

}