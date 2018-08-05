// Dxflib
// GeometricEntityBase.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-05-8:05 AM
// By: Adam Renaud
// 
// ============================================================

namespace Dxflib.Geometry
{
    /// <summary>
    ///     An abstract class that serves two purposes:
    ///     1. To unify all geometric properties
    ///     2. To provide the GeometryChanged Event that
    ///     Occurs when there is a geometry change to alert parent classes
    ///     that they might need to update their geometry
    /// </summary>
    public abstract class GeometricEntityBase
    {
        /// <summary>
        ///     The Geometry changed event that alerts subscribers that they might need
        ///     to update their geometry.
        /// </summary>
        public event GeometryChangedHandler GeometryChanged;

        /// <summary>
        ///     Base class Invokation of the Geometry changed event
        /// </summary>
        /// <param name="args">Arguments for the event</param>
        protected virtual void OnGeometryChanged(GeometryChangedHandlerArgs args)
        {
            GeometryChanged?.Invoke(this, args);
        }
    }

    /// <summary>
    ///     The delegate for the geometry changed event
    /// </summary>
    /// <param name="sender">The sending object that invoked the event</param>
    /// <param name="args">The event arguments</param>
    public delegate void GeometryChangedHandler(object sender, GeometryChangedHandlerArgs args);

    /// <summary>
    ///     The class that defines the event arguments
    /// </summary>
    public class GeometryChangedHandlerArgs
    {
        /// <summary>
        ///     The name of the affected argument. Could be X, Y, Z etc..
        /// </summary>
        /// <param name="name">The name that becomes the Name property in the class</param>
        public GeometryChangedHandlerArgs(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     The vertex ID that was changed in the geometry event
        /// </summary>
        /// <param name="vertexId">Vertex ID could be 0 or 1 for a geoline</param>
        public GeometryChangedHandlerArgs(int vertexId)
        {
            VertexId = vertexId;
            Name = "";
        }

        /// <summary>
        ///     The Name strig
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Vertex ID
        /// </summary>
        public int VertexId { get; }
    }
}