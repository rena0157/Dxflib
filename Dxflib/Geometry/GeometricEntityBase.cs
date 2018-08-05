using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.Geometry
{
    public abstract class GeometricEntityBase
    {
        public event GeometryChangedHandler GeometryChanged;

        protected virtual void OnGeometryChanged(GeometryChangedHandlerArgs args)
        {
            GeometryChanged?.Invoke(this, args);
        }
    }

    public delegate void GeometryChangedHandler(object sender, GeometryChangedHandlerArgs args);

    public class GeometryChangedHandlerArgs
    {
        public string Name { get; }
        public GeometryChangedHandlerArgs(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Vertex ID
        /// </summary>
        public int VertexId { get; }

        public GeometryChangedHandlerArgs(int vertexId)
        {
            VertexId = vertexId;
            Name = "";
        }
    }
}
