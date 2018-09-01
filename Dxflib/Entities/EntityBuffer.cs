// Dxflib
// EntityBuffer.cs
// 
// ============================================================
// 
// Created: 2018-08-30
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using Dxflib.IO;

namespace Dxflib.Entities
{
    /// <summary>
    ///     The Entity buffer class that is used only in extraction
    /// </summary>
    public class EntityBuffer
    {
        /// <summary>
        ///     The Entity Type
        /// </summary>
        public EntityTypes EntityType;

        /// <summary>
        ///     Main Constructor that resets all values
        /// </summary>
        protected EntityBuffer()
        {
            Handle = "";
            LayerName = "";
            EntityType = EntityTypes.None;
            EntityReferenceList = new List<string>();
        }

        /// <summary>
        ///     The Entity's Handle
        /// </summary>
        public string Handle { get; set; }

        /// <summary>
        ///     The Entity's Layer Name
        /// </summary>
        public string LayerName { get; set; }

        /// <summary>
        /// </summary>
        public List<string> EntityReferenceList { get; protected set; }

        /// <summary>
        ///     The Parse Virtual Function that is to be overriden by
        ///     each entity that is to be extracted. This function also,
        ///     Parses global entity properties such as handle or <see cref="LayerName" />.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>True if the parse was successful</returns>
        public virtual bool Parse(TaggedDataList list, int index)
        {
            var currentData = list.GetPair(index);
            switch ( currentData.GroupCode )
            {
                case GroupCodesBase.Handle:
                    Handle = currentData.Value;
                    return true;
                case GroupCodesBase.LayerName:
                    LayerName = currentData.Value;
                    return true;
                default:
                    return false;
            }
        }
    }
}