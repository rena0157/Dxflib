namespace Dxflib.Geometry
{
    /// <summary>
    ///     The GeoArc Class, a class that defines an arc. Note that
    ///     an arc can be defined in more than one way. If this GeoArc is a
    ///     part of an LwPolyLine then is defined using a bulge. If it is
    ///     alone then it is defined using a radius.
    /// </summary>
    public class GeoArc : GeometricEntityBase
    {
        private double _bulge;
        private readonly Vertex _vertex0;
        private readonly Vertex _vertex1;

        /// <summary>
        ///     GeoArc Constructor
        /// </summary>
        /// <param name="vertex0">First Vertex</param>
        /// <param name="vertex1">Second Vertex</param>
        /// <param name="bulge">The Bulge</param>
        public GeoArc(Vertex vertex0, Vertex vertex1, double bulge)
        {
            // Set type
            GeometryEntityType = GeometryEntityTypes.GeoArc;

            // Set Variables
            _vertex0 = vertex0;
            _vertex1 = vertex1;
            _bulge = bulge;

            // Subscribe to events
            _vertex0.GeometryChanged += UpdateGeometry;
            _vertex1.GeometryChanged += UpdateGeometry;


            // Calculate Geometry
            UpdateGeometry(this, new GeometryChangedHandlerArgs(""));
        }

        /// <summary>
        ///     The Lenth of the GeoArc
        /// </summary>
        public double Length { get; private set; }

        public double Radius { get; private set; }

        /// <summary>
        ///     The Length of the GeoArc
        /// </summary>
        /// <returns>A double which represents the length</returns>
        protected override double CalcLength() => GeoMath.Distance(
            _vertex0, _vertex1, _bulge);

        /// <summary>
        /// The Radius that is defined by the bulge
        /// </summary>
        /// <returns>The Radius</returns>
        private double CalcRadius()
        {
            return 0;
        }

        /// <summary>
        ///     Update the Geometry of the GeoArc
        /// </summary>
        /// <param name="sender">Usually this</param>
        /// <param name="args">Arguments</param>
        private void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            Length = CalcLength();
        }
    }
}