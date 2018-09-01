// Dxflib
// FileSectionBase.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;

namespace Dxflib.IO
{
    /// <summary>
    ///     File Section base is a class that represents the different sections of an AutoCAD Class
    /// </summary>
    public class FileSectionBase
    {
        /// <summary>
        ///     The List of Tagged data (Group Codes and values)
        /// </summary>
        protected readonly TaggedDataList DataList;

        /// <summary>
        ///     The Main Constructor for the file section base which records
        ///     its starting position in the file
        /// </summary>
        /// <param name="startingIndex">The Starting index of the file</param>
        /// <param name="list">The List of Tagged Data</param>
        protected FileSectionBase(int startingIndex, TaggedDataList list)
        {
            DataList = list;
            StartIndex = startingIndex;
        }

        /// <summary>
        ///     The Starting Index of the file section
        /// </summary>
        protected int StartIndex { get; }

        /// <summary>
        ///     The Ending Index of the file section
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        protected int EndIndex { get; set; }

        /// <summary>
        ///     A virtual class that is designed to extract the data from the section.
        ///     Note that the End Index is only ever recorded at the calling of this virtual function
        /// </summary>
        public virtual void ReadSection() { }
    }


    /// <summary>
    ///     The File Section Enumeration that holds Enumerations for
    ///     the different section of a dxf file
    /// </summary>
    public enum FileSections
    {
        /// <summary>
        ///     The Header Section
        /// </summary>
        [Description("HEADER")] Header,

        /// <summary>
        ///     The classes section
        /// </summary>
        [Description("CLASSES")] Classes,

        /// <summary>
        ///     The tables section
        /// </summary>
        [Description("TABLES")] Tables,

        /// <summary>
        ///     The Blocks section
        /// </summary>
        [Description("BLOCKS")] Blocks,

        /// <summary>
        ///     The Entities Section
        /// </summary>
        [Description("ENTITIES")] Entities,

        /// <summary>
        ///     The Objects section
        /// </summary>
        [Description("OBJECTS")] Objects,

        /// <summary>
        ///     If no section is selected then this is the
        ///     effective null value
        /// </summary>
        [Description("None")] None
    }

    /// <summary>
    ///     A class that will contain all of the <see cref="FileSectionBase" /> Starting markers
    ///     These starting markers will signify the starting location of a new section in the
    ///     dxf file.
    /// </summary>
    public static class FileSectionStartMarkers
    {
        /// <summary>
        ///     The Header Section Starting Marker
        /// </summary>
        public const string Header = "HEADER";

        /// <summary>
        ///     The Classes Section Starting Marker
        /// </summary>
        public const string Classes = "CLASSES";

        /// <summary>
        ///     The Tables Section Starting Marker
        /// </summary>
        public const string Tables = "TABLES";

        /// <summary>
        ///     The Blocks Section Starting Marker
        /// </summary>
        public const string Blocks = "BLOCKS";

        /// <summary>
        ///     The Entities Section Starting Marker
        /// </summary>
        public const string Entities = "ENTITIES";

        /// <summary>
        ///     The Objects Section Starting Marker
        /// </summary>
        public const string Objects = "OBJECTS";
    }
}