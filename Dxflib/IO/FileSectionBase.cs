// Dxflib
// FileSectionBase.cs
// 
// ============================================================
// 
// Created: 2018-08-19
// Last Updated: 2018-08-19-7:12 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;

namespace Dxflib.IO
{
    /// <summary>
    /// File Section base is a class that represents the different sections of an AutoCAD Class
    /// </summary>
    public class FileSectionBase
    {
        /// <summary>
        /// The List of Tagged data (Group Codes and values)
        /// </summary>
        protected TaggedDataList DataList;

        /// <summary>
        /// The Main Constructor for the file section base which records
        /// its starting position in the file
        /// </summary>
        /// <param name="startingIndex">The Starting index of the file</param>
        /// <param name="list">The List of Tagged Data</param>
        public FileSectionBase(int startingIndex, TaggedDataList list)
        {
            DataList = list;
            StartIndex = startingIndex;
        }

        /// <summary>
        /// The File Section Type
        /// </summary>
        public FileSections FileSection { get; protected set; }

        /// <summary>
        /// The Starting Index of the file section
        /// </summary>
        public int StartIndex { get; protected set; }

        /// <summary>
        /// The Ending Index of the file section
        /// </summary>
        public int EndIndex { get; set; }

        /// <summary>
        /// A virtual class that is designed to extract the data from the section.
        /// Note that the End Index is only ever recorded at the calling of this virtual function
        /// </summary>
        public virtual void ReadSection() { EndIndex = DataList.Index; }
    }


    /// <summary>
    ///     The File Section Enumeration that holds Enumerations for
    ///     the different section of a dxf file
    /// </summary>
    public enum FileSections
    {
        [Description("HEADER")] Header,

        [Description("CLASSES")] Classes,

        [Description("TABLES")] Tables,

        [Description("BLOCKS")] Blocks,

        [Description("ENTITIES")] Entities,

        [Description("OBJECTS")] Objects,

        [Description("None")] None
    }
}