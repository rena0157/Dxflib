using Dxflib.IO.GroupCodes;

namespace Dxflib.IO.Header
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
}