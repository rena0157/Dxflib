// Dxflib
// LinePointer.cs
// 
// ============================================================
// 
// Created: 2018-08-28
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.Entities;

namespace Dxflib.AcadEntities.Pointer
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class LinePointer : EntityPointer<Line>
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="handle"></param>
        public LinePointer(string handle) : base(handle) { }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override Line RefEntity
        {
            set
            {
                Handle = value.Handle;
                _entity = value;
            }
        }
    }
}