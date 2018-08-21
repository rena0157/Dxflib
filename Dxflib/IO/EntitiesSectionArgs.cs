using System;
using System.Collections.Generic;
using System.Text;
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

        /// <summary>
        /// 
        /// </summary>
        public override void ReadSection()
        {
            for ( var currentData = DataList.Next; 
                currentData.Value != GroupCodesBase.EndSecionMarker;
                currentData = DataList.Next)
            {
                switch ( currentData.Value )
                {
                    case LineGroupCodes.StartMarker:
                        _lineBuffer = new LineBuffer();
                        _lineBuffer.Parse(DataList);
                        Entities.Add(new Line(_lineBuffer));
                        break;
                    case LwPolylineCodes.StartMarker:
                        _lwPolyLineBuffer = new LwPolyLineBuffer();
                        _lwPolyLineBuffer.Parse(DataList);
                        Entities.Add(new LwPolyLine(_lwPolyLineBuffer));
                        break;
                    default:
                        continue;
                }
            }
            base.ReadSection();
        }
    }
}
