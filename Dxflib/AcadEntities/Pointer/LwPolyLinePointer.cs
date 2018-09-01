// Dxflib
// LwPolyLinePointer.cs
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
    ///     The LwPolyline Pointer
    /// </summary>
    public class LwPolyLinePointer : EntityPointer<LwPolyLine>
    {
        /// <inheritdoc />
        /// <summary>
        ///     The main constructor for the LwPolyline pointer
        ///     This constructor will not
        /// </summary>
        /// <param name="handle"></param>
        public LwPolyLinePointer(string handle) : base(handle) { }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override LwPolyLine RefEntity
        {
            set
            {
                Handle = value.Handle;
                _entity = value;
            }
        }
    }
}