// Dxflib
// TaggedDataList.cs
// 
// ============================================================
// 
// Created: 2018-08-21
// Last Updated: 2018-08-21-8:54 PM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;

namespace Dxflib.IO
{

    public class TaggedDataList
    {
        private readonly List<TaggedData> _list;

        /// <summary>
        /// </summary>
        /// <param name="stringList"></param>
        public TaggedDataList(string[] stringList)
        {
            _list = new List<TaggedData>(stringList.Length / 2);
            for ( var i = 0; i < stringList.Length - 2; i += 2 )
                _list.Add(new TaggedData(stringList[i], stringList[i + 1]));
        }

        /// <summary>
        ///     Total Length of the list
        /// </summary>
        public int Length => _list.Count;

        /// <summary>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TaggedData GetPair(int index) { return _list[index]; }
    }
}