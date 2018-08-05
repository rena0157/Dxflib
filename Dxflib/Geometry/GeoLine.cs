// Dxflib
// GeoLine.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-05-7:59 AM
// By: Adam Renaud
// 
// ============================================================

namespace Dxflib.Geometry
{
    /// <summary>
    ///     A Geometric line. This line is different than a line that is
    ///     inherienting the entity class. This line should only be used for geometric
    ///     purposes
    /// </summary>
    public class GeoLine : GeometricEntityBase
    {
        private Vertex _vertex0;
        private Vertex _vertex1;

        /// <summary>
        ///     Main Constructor of the GeoLine
        /// </summary>
        /// <param name="v0">The First Vertex</param>
        /// <param name="v1">The Second Vertex</param>
        public GeoLine(Vertex v0, Vertex v1)
        {
            // Set type
            GeometryEntityType = GeometryEntityTypes.GeoLine;

            // Set Variables
            _vertex0 = v0;
            _vertex1 = v1;

            // Subscribe to events
            _vertex0.GeometryChanged += UpdateGeometry;
            _vertex1.GeometryChanged += UpdateGeometry;

            // Calculate geometry
            Length = CalcLength();
        }

        /// <summary>
        ///     The first Vertex. Note that when setting this property a GeometryChanged
        ///     event will be broadcasted. Also changing this property will cause an update
        ///     Geometry method to happen.
        /// </summary>
        public Vertex Vertex0
        {
            get => _vertex0;
            set
            {
                _vertex0 = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs(0));
                UpdateGeometry(this, new GeometryChangedHandlerArgs(0));
            }
        }

        /// <summary>
        ///     The same as <see cref="Vertex0" />. Just the second vertex.
        /// </summary>
        public Vertex Vertex1
        {
            get => _vertex1;
            set
            {
                _vertex1 = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs(1));
                UpdateGeometry(this, new GeometryChangedHandlerArgs(1));
            }
        }

        /// <summary>
        /// Overrided CalcLength Function
        /// </summary>
        /// <returns></returns>
        protected override double CalcLength() => GeoMath.Distance(Vertex0, Vertex1);

        /// <summary>
        ///     The total length of the polyline.
        /// </summary>
        public double Length { get; private set; }

        private void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            Length = CalcLength();
        }
    }
}