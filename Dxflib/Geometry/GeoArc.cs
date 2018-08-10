// Dxflib
// GeoArc.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-08-07-7:12 PM
// By: Adam Renaud
// 
// ============================================================

using Dxflib.LinAlg;

namespace Dxflib.Geometry
{
    /// <inheritdoc />
    /// <summary>
    ///     The GeoArc Class, a class that defines an arc. Note that
    ///     an arc can be defined in more than one way. If this GeoArc is a
    ///     part of an LwPolyLine then is defined using a bulge. If it is
    ///     alone then it is defined using a radius.
    /// </summary>
    public class GeoArc : GeometricEntityBase
    {
        private double _bulge;
        private Vertex _vertex0;
        private Vertex _vertex1;

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

        /// <summary>
        ///     The Total Angle of the Arc
        /// </summary>
        public double Angle { get; private set; }

        /// <summary>
        ///     The Radius of the Arc
        /// </summary>
        public double Radius { get; private set; }

        public double Area { get; private set; }

        /// <summary>
        ///     The First Vertex
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
        ///     The Second Vertex
        /// </summary>
        public Vertex Vertex1
        {
            get => _vertex1;
            set
            {
                _vertex1 = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs(1));
                UpdateGeometry(this, new GeometryChangedHandlerArgs(0));
            }
        }

        /// <summary>
        ///     The bulge value for this object (Is similar to the curvature of an arc)
        /// </summary>
        public double BulgeValue
        {
            get => _bulge;
            set
            {
                _bulge = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Bulge"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Bulge"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector ToVector()
        {
            // Todo: Test This
            return new Vector(_vertex0, _vertex1);
        }

        /// <summary>
        ///     The Length of the GeoArc
        /// </summary>
        /// <returns>A double which represents the length</returns>
        protected override double CalcLength()
        {
            return GeoMath.Distance(
                _vertex0, _vertex1, _bulge);
        }

        /// <summary>
        ///     The Radius that is defined by the bulge
        /// </summary>
        /// <returns>The Radius</returns>
        private double CalcRadius() { return Bulge.Radius(_vertex0, _vertex1, Angle); }

        /// <summary>
        ///     Calculate the angle of the arc
        /// </summary>
        /// <returns></returns>
        private double CalcAngle() { return Bulge.Angle(_bulge); }

        /// <summary>
        ///     Calcuate the area of the Geoarc
        /// </summary>
        /// <returns></returns>
        private double CalcArea() { return GeoMath.ChordArea(this); }

        /// <summary>
        ///     Update the Geometry of the GeoArc
        /// </summary>
        /// <param name="sender">Usually this</param>
        /// <param name="args">Arguments</param>
        protected sealed override void UpdateGeometry(
            object sender, GeometryChangedHandlerArgs args)
        {
            Length = CalcLength();
            Angle = CalcAngle();
            Radius = CalcRadius();
            Area = CalcArea();
        }
    }
}