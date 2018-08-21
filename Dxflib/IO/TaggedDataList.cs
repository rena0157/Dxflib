using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.IO
{
    public class TaggedDataList
    {
        private List<TaggedData> _list;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringList"></param>
        public TaggedDataList(string[] stringList)
        {
            _list = new List<TaggedData>(stringList.Length/2);
            for ( int i = 0; i < stringList.Length - 2; i +=2 )
            {
                _list.Add(new TaggedData(stringList[i], stringList[i + 1]));
            }
                
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Where the pointer is at
        /// </summary>
        public TaggedData CurrentData => _list[Index];

        /// <summary>
        /// Total Length of the list
        /// </summary>
        public int Length => _list.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TaggedData GetPair(int index) => _list[index];

        /// <summary>
        /// Get the next pair
        /// </summary>
        public TaggedData Next 
            => Index < _list.Count - 1 ? _list[++Index] : new TaggedData("  0", "EOF");
    }
}
