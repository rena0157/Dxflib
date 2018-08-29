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
        public LinePointer(string handle) : base(handle)
        {
        }

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