using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.IO
{
    /// <summary>
    /// TaggedData is a class that is designed to replicate the tagged data of a dxf file
    /// </summary>
    public class TaggedData
    {
        private readonly Tuple<string, string> _groupCodeTuple;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupCodeTuple"></param>
        public TaggedData(Tuple<string, string> groupCodeTuple) { _groupCodeTuple = groupCodeTuple; }

        /// <summary>
        /// 
        /// </summary>
        public TaggedData(string groupCode, string value)
        {
            _groupCodeTuple = new Tuple<string, string>(groupCode, value);
        }

        /// <summary>
        /// The Group Code
        /// </summary>
        public string GroupCode => _groupCodeTuple.Item1;

        /// <summary>
        /// 
        /// </summary>
        public Type GetValueType => DxfTypeConverter.ConvertType(GroupCode);

        /// <summary>
        /// The Group Code Value
        /// </summary>
        public string Value => _groupCodeTuple.Item2;
    }
}
