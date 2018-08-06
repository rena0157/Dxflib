using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.Geometry
{
    public class GeoArc : GeometricEntityBase
    {
        private Vertex _vertex0;
        private Vertex _vertex1;
        private double _bulge;

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

        }

        public double Length { get; private set; }

        protected override double CalcLength()
        {
            return base.CalcLength();
        }

        private void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {

        }
    }
}
