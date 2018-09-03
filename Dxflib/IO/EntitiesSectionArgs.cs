// Dxflib
// EntitiesSectionArgs.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-03-11:21 AM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using Dxflib.Entities;
using Dxflib.Entities.Hatch;
using Dxflib.Entities.Text;
using Dxflib.IO.GroupCodes;

namespace Dxflib.IO
{
    /// <inheritdoc />
    /// <summary>
    ///     The Entities section arguments class. A class that stores all
    ///     of the data from the entities section of the dxf file.
    /// </summary>
    public class EntitiesSectionArgs : FileSectionBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     The Main Constructor for the section arguments class.
        ///     This constructor will initialize all required private and public variables
        /// </summary>
        /// <param name="startingIndex">The starting index of the section</param>
        /// <param name="list">
        ///     The <see cref="T:Dxflib.IO.TaggedDataList" /> list that contains
        ///     all of the tagged data
        /// </param>
        public EntitiesSectionArgs(int startingIndex, TaggedDataList list)
            : base(startingIndex, list)
        {
            Entities = new List<Entity>();
        }

        /// <summary>
        ///     The Entities List
        /// </summary>
        public List<Entity> Entities { get; }

        /// <inheritdoc />
        /// <summary>
        ///     This section when called will read the data that is contained in
        ///     the list that was passed in from the constructor.
        /// </summary>
        public override void ReadSection()
        {
            // Iterate through the list of tagged data
            for ( var currentIndex = StartIndex;
                currentIndex < DataList.Length;
                ++currentIndex )
            {
                // set the current data
                var currentData = DataList.GetPair(currentIndex);

                // Set the ending index of the file and break out of the loop
                if ( currentData.GroupCode == GroupCodesBase.EndSectionMarker )
                {
                    EndIndex = currentIndex;
                    break;
                }

                // The master switch for the values
                switch ( currentData.Value )
                {
                    /*
                     * All Entities Cases will follow the same formula:
                     * 1. Create a new Buffer class
                     * 2. pass the Data list to the buffer class with the starting
                     *    index of the entity.
                     * 3. Add the entity to the entity list
                     */

                    // LINE
                    case LineGroupCodes.StartMarker:
                        var lineBuffer = new LineBuffer();
                        lineBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new Line(lineBuffer));
                        continue;

                    // LWPOLYLINE
                    case LwPolylineCodes.StartMarker:
                        var lwPolyLineBuffer = new LwPolyLineBuffer();
                        lwPolyLineBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new LwPolyLine(lwPolyLineBuffer));
                        continue;

                    // HATCH
                    case HatchCodes.StartMarker:
                        var hatchBuffer = new HatchBuffer();
                        hatchBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new Hatch(hatchBuffer));
                        continue;

                    // ARC
                    case CircularArcCodes.StartMarker:
                        var arcBuffer = new CircularArcBuffer();
                        arcBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new CircularArc(arcBuffer));
                        continue;

                    // TEXT
                    case TextCodes.StartMarker:
                        var textBuffer = new TextBuffer();
                        textBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new Text(textBuffer));
                        continue;

                    // MTEXT
                    case MTextCodes.StartMarker:
                        var mTextBuffer = new MTextBuffer();
                        mTextBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new MText(mTextBuffer));
                        continue;

                    // POINT
                    case PointCodes.StartMarker:
                        var pointBuffer = new PointBuffer();
                        pointBuffer.Parse(DataList, currentIndex);
                        Entities.Add(new Point(pointBuffer));
                        continue;

                    // DEFAULT
                    default:
                        continue;
                }
            }
        }
    }
}