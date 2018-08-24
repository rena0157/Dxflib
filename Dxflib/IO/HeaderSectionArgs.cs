// Dxflib
// HeaderSectionArgs.cs
// 
// ============================================================
// 
// Created: 2018-08-23
// Last Updated: 2018-08-23-9:54 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;

namespace Dxflib.IO
{
    /// <inheritdoc cref="FileSectionBase" />
    /// <summary>
    /// </summary>
    public class HeaderSectionArgs : FileSectionBase
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="startingIndex"></param>
        /// <param name="list"></param>
        public HeaderSectionArgs(int startingIndex, TaggedDataList list)
            : base(startingIndex, list)
        {
            AutoCadVersion = new AutoCadVersionVar(string.Empty);
            LastSavedBy = new StringVar(FileVariableCodes.LastSavedBy, string.Empty);
            CurrentLayer = new StringVar(FileVariableCodes.CurrentLayer, string.Empty);
        }

        /// <summary>
        ///     What the AutoCAD Version is
        /// </summary>
        public AutoCadVersionVar AutoCadVersion { get; }

        /// <summary>
        ///     Who the file was last saved by
        /// </summary>
        public StringVar LastSavedBy { get; }

        /// <summary>
        ///     The CurrentLayer
        /// </summary>
        public StringVar CurrentLayer { get; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override void ReadSection()
        {
            for ( var currentIndex = StartIndex;
                currentIndex < DataList.Length;
                ++currentIndex )
            {
                var currentData = DataList.GetPair(currentIndex);
                EndIndex = currentIndex;
                if ( currentData.Value == GroupCodesBase.EndSectionMarker )
                    break;

                switch ( currentData.Value )
                {
                    case FileVariableCodes.AutoCadVersion:
                        currentData = DataList.GetPair(++currentIndex);
                        AutoCadVersion.Value = AutoCadVersionVar.ParseAutoCadVersion(currentData.Value);
                        continue;
                    case FileVariableCodes.LastSavedBy:
                        currentData = DataList.GetPair(++currentIndex);
                        LastSavedBy.Value = currentData.Value;
                        continue;

                    case FileVariableCodes.CurrentLayer:
                        currentData = DataList.GetPair(++currentIndex);
                        CurrentLayer.Value = currentData.Value;
                        continue;
                    default:
                        continue;
                }
            }
        }
    }

    /// <summary>
    ///     AutoCAD Versions up to 2013
    /// </summary>
    public enum AutoCadVersions
    {
        /// <summary>
        ///     AutoCAD Version R10
        /// </summary>
        [Description("R10")] AC1006,

        /// <summary>
        ///     AutoCAD version R11 and R12
        /// </summary>
        [Description("R11 and R12")] AC1009,

        /// <summary>
        ///     AutoCAD Version R13
        /// </summary>
        [Description("R13")] AC1012,

        /// <summary>
        ///     AutoCAD Version R14
        /// </summary>
        [Description("R14")] AC1014,

        /// <summary>
        ///     AutoCAD Version 2000
        /// </summary>
        [Description("AutoCAD 2000")] AC1015,

        /// <summary>
        ///     AutoCAD Version 2004
        /// </summary>
        [Description("AutoCAD 2004")] AC1018,

        /// <summary>
        ///     AutoCAD Version 2007
        /// </summary>
        [Description("AutoCAD 2007")] AC1021,

        /// <summary>
        ///     AutoCAD Version 2010
        /// </summary>
        [Description("AutoCAD 2010")] AC1024,

        /// <summary>
        ///     AutoCAD Version 2013
        /// </summary>
        [Description("AutoCAD 2013")] AC1027,

        /// <summary>
        ///     The AutoCAD Version is Unknown
        /// </summary>
        [Description("Unknown")] Unknown
    }
}