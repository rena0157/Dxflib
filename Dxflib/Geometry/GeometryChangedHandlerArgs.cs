namespace Dxflib.Geometry
{
    /// <summary>
    ///     The class that defines the event arguments
    /// </summary>
    public class GeometryChangedHandlerArgs
    {
        /// <summary>
        ///     The name of the affected argument. Could be X, Y, Z etc..
        /// </summary>
        /// <param name="name">The name that becomes the Name property in the class</param>
        public GeometryChangedHandlerArgs(string name) { Name = name; }

        /// <summary>
        ///     The vertex ID that was changed in the geometry event
        /// </summary>
        /// <param name="vertexId">Vertex ID could be 0 or 1 for a geoline</param>
        // ReSharper disable once UnusedParameter.Local
        public GeometryChangedHandlerArgs(int vertexId) { Name = ""; }

        /// <summary>
        ///     The Name string
        /// </summary>
        public string Name { get; }
    }
}