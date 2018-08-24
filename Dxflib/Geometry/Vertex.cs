namespace Dxflib.Geometry
{
    public class Vertex : GeometricEntityBase
    {
        private double _x;
        private double _y;
        private double _z;

        /// <summary>
        ///     Vertex Constructor
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        public Vertex(double x, double y, double z = 0)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        ///     x coordinate, note that when setting this value
        ///     the <see cref="GeometryChangedHandler" /> Event will broadcast
        /// </summary>
        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("X"));
            }
        }

        /// <summary>
        ///     y coodinate, note that when setting to a new value
        ///     a geometry event will be raised
        /// </summary>
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Y"));
            }
        }

        /// <summary>
        ///     z coordinate, note that when setting to a new value
        ///     a geometry event will be raised
        /// </summary>
        public double Z
        {
            get => _z;
            set
            {
                _z = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Z"));
            }
        }

    }
}