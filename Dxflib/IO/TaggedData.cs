// Dxflib
// TaggedData.cs
// 
// ============================================================
// 
// Created: 2018-08-21
// Last Updated: 2018-08-21-8:54 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.IO
{
    /// <summary>
    ///     TaggedData is a class that is designed to replicate the tagged data of a dxf file
    /// </summary>
    public class TaggedData
    {
        private readonly Tuple<string, string> _groupCodeTuple;

        /// <summary>
        /// </summary>
        /// <param name="groupCodeTuple"></param>
        public TaggedData(Tuple<string, string> groupCodeTuple) { _groupCodeTuple = groupCodeTuple; }

        /// <summary>
        /// </summary>
        public TaggedData(string groupCode, string value)
        {
            _groupCodeTuple = new Tuple<string, string>(groupCode, value);
        }

        /// <summary>
        ///     The Group Code <see cref="GroupCodesBase"/>
        ///     for a list of group codes
        /// </summary>
        public string GroupCode => _groupCodeTuple.Item1;

        /// <summary>
        /// </summary>
        public Type GetValueType => DxfTypeConverter.ConvertType(GroupCode);

        /// <summary>
        ///     The Group Code Value
        /// </summary>
        public string Value => _groupCodeTuple.Item2;
    }
}