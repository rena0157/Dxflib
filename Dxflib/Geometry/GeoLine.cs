// Dxflib
// GeoLine.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.LinAlg;

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
            Area = CalcArea();
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
        ///     The total length of the polyline.
        /// </summary>
        public double Length { get; private set; }

        /// <summary>
        ///     The Area of the GeoLine
        /// </summary>
        public double Area { get; private set; }

        /// <summary>
        ///     Overrided CalcLength Function
        /// </summary>
        /// <returns></returns>
        protected sealed override double CalcLength() { return GeoMath.Distance(Vertex0, Vertex1); }

        /// <summary>
        ///     Calculate the total area underneath this line and the x-axis
        /// </summary>
        /// <returns>The Area of the line and the x axis</returns>
        private double CalcArea() { return GeoMath.TrapzArea(this); }

        /// <summary>
        ///     Updates the Geometry for this class
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="args"></param>
        protected override void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            Length = CalcLength();
            Area = CalcArea();
        }

        /// <summary>
        ///     Convert this Geoline to a Vector
        /// </summary>
        /// <returns>A new Vector</returns>
        public Vector ToVector()
        {
            // Todo: Test This
            return new Vector(this);
        }
    }
}