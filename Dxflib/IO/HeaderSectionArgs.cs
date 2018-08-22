using System.ComponentModel;

namespace Dxflib.IO
{

    /// <inheritdoc cref="FileSectionBase"/>
    /// <summary>
    ///
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
            FileSection = FileSections.Header;
            AutoCadVersion = new AutoCadVersionVar(string.Empty);
            LastSavedBy = new StringVar(FileVariableCodes.LastSavedBy, string.Empty);
            CurrentLayer = new StringVar(FileVariableCodes.CurrentLayer, string.Empty);
        }

        /// <summary>
        /// What the AutoCAD Version is
        /// </summary>
        public AutoCadVersionVar AutoCadVersion { get; }

        /// <summary>
        /// Who the file was last saved by
        /// </summary>
        public StringVar LastSavedBy { get; }

        /// <summary>
        /// The CurrentLayer
        /// </summary>
        public StringVar CurrentLayer { get; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override void ReadSection()
        {
            for (var currentIndex = StartIndex;
                currentIndex < DataList.Length; ++currentIndex)
            {
                var currentData = DataList.GetPair(currentIndex);
                EndIndex = currentIndex;
                if (currentData.Value == GroupCodesBase.EndSecionMarker)
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
