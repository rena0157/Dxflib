using System.Collections.Generic;
using Dxflib.Entities;

namespace Dxflib.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class EntitiesSectionArgs : FileSectionBase
    {
        private LineBuffer _lineBuffer;
        private LwPolyLineBuffer _lwPolyLineBuffer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startingIndex"></param>
        /// <param name="list"></param>
        public EntitiesSectionArgs(int startingIndex, TaggedDataList list)
            : base(startingIndex, list)
        {
            Entities = new List<Entity>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Entity> Entities { get; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override void ReadSection()
        {
            for (var currentIndex = StartIndex;
                currentIndex < DataList.Length;
                ++currentIndex)
            {
                var currentData = DataList.GetPair(currentIndex);
                if (currentData.GroupCode == GroupCodesBase.EndSecionMarker)
                    break;

                switch ( currentData.Value )
                {
                    case LineGroupCodes.StartMarker:
                        _lineBuffer = new LineBuffer();
                        _lineBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new Line(_lineBuffer));
                        continue;
                    case LwPolylineCodes.StartMarker:
                        _lwPolyLineBuffer = new LwPolyLineBuffer();
                        _lwPolyLineBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new LwPolyLine(_lwPolyLineBuffer));
                        continue;
                    default:
                        continue;
                }
            }
            base.ReadSection();
        }
    }
}
