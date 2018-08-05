namespace Dxflib.Geometry
{
    public class GeoLine : GeometricEntityBase
    {
        private Vertex _vertex0;
        private Vertex _vertex1;

        public GeoLine(Vertex v0, Vertex v1)
        {
            // Set Variables
            _vertex0 = v0;
            _vertex1 = v1;

            // Subscribe to events
            _vertex0.GeometryChanged += UpdateGeometry;
            _vertex1.GeometryChanged += UpdateGeometry;

            // Calculate geometry
            Length = GeoMath.Distance(v0, v1);
        }

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

        public double Length { get; private set; }

        private void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            Length = GeoMath.Distance(Vertex0, Vertex1);
        }
    }
}