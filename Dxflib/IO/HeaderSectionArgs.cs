using System;
using System.Collections.Generic;
using System.Text;
using Dxflib.Parser;

namespace Dxflib.IO
{

    /// <inheritdoc cref="FileSectionBase"/>
    /// <summary>
    ///
    /// </summary>
    public class HeaderSectionArgs : FileSectionBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startingIndex"></param>
        /// <param name="list"></param>
        public HeaderSectionArgs(int startingIndex, TaggedDataList list)
            : base(startingIndex, list)
        {
            FileSection = FileSections.Header;
            AutoCadVersion = new AutoCadVersionVar(string.Empty);
            LastSavedBy = new StringVar(FileVariableCodes.LastSavedBy, string.Empty);
        }

        /// <summary>
        /// What the AutoCAD Version is
        /// </summary>
        public AutoCadVersionVar AutoCadVersion { get; set; }

        /// <summary>
        /// Who the file was last saved by
        /// </summary>
        public StringVar LastSavedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void ReadSection()
        {
            var currentIndex = StartIndex;
            for (var currentData = DataList.GetPair(currentIndex);
                currentData.Value != GroupCodesBase.EndSecionMarker && currentIndex < DataList.Length; ++currentIndex)
            {

                if ( currentData.Value == FileVariableCodes.AutoCadVersion )
                {
                    currentData = DataList.GetPair(++currentIndex);
                    AutoCadVersion.Value = AutoCadVersionVar.ParseAutoCadVersion(currentData.Value);
                }
                else if ( currentData.Value == FileVariableCodes.LastSavedBy )
                {
                    currentData = DataList.GetPair(++currentIndex);
                    LastSavedBy.Value = currentData.Value;
                }

            }

            EndIndex = currentIndex;
        }
    }
}
