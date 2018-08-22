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
        /// Total Length of the list
        /// </summary>
        public int Length => _list.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TaggedData GetPair(int index) => _list[index];
    }
}
