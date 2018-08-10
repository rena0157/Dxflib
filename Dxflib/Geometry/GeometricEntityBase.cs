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
        /// The entity type
        /// </summary>
        public GeometryEntityTypes GeometryEntityType { get; protected set; }

        /// <summary>
        ///     Base class Invocation of the Geometry changed event
        /// </summary>
        /// <param name="args">Arguments for the event</param>
        protected virtual void OnGeometryChanged(GeometryChangedHandlerArgs args)
        {
            GeometryChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Protected Virtual void function that should define how the geometry entity
        /// deals with geometry update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {

        }

        /// <summary>
        /// Calculating the length of the geometric entity
        /// </summary>
        /// <returns>In the base entity it returns 0</returns>
        protected virtual double CalcLength()
        {
            return 0;
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
        // ReSharper disable once UnusedParameter.Local
        public GeometryChangedHandlerArgs(int vertexId)
        {
            Name = "";
        }

        /// <summary>
        ///     The Name string
        /// </summary>
        public string Name { get; }
    }

    /// <summary>
    /// The different geometry entity types
    /// </summary>
    public enum GeometryEntityTypes
    {
        /// <summary>
        /// <see cref="Geometry.Vertex"/>
        /// </summary>
        Vertex,

        /// <summary>
        /// <see cref="Dxflib.Geometry.GeoLine"/>
        /// </summary>
        GeoLine,

        /// <summary>
        /// <see cref="Dxflib.Geometry.GeoArc"/>
        /// </summary>
        GeoArc
    }
}