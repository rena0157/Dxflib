using Dxflib.Entities;

namespace Dxflib.AcadEntities.Pointer
{
    /// <inheritdoc />
    /// <summary>
    /// The LwPolyline Pointer
    /// </summary>
    public class LwPolyLinePointer : EntityPointer<LwPolyLine>
    {
        /// <inheritdoc />
        /// <summary>
        /// The main constructor for the LwPolyline pointer
        /// This constructor will not 
        /// </summary>
        /// <param name="handle"></param>
        public LwPolyLinePointer(string handle) : base(handle)
        {
        }

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
