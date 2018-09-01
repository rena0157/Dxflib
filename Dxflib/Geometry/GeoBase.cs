// Dxflib
// GeoBase.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-1:09 PM
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
    public abstract class GeoBase
    {
        /// <summary>
        ///     The entity type
        /// </summary>
        public GeometryEntityTypes GeometryEntityType { get; protected set; }

        /// <summary>
        ///     The Geometry changed event that alerts subscribers that they might need
        ///     to update their geometry.
        /// </summary>
        public event GeometryChangedHandler GeometryChanged;

        /// <summary>
        ///     Base class Invocation of the Geometry changed event
        /// </summary>
        /// <param name="args">Arguments for the event</param>
        protected virtual void OnGeometryChanged(GeometryChangedHandlerArgs args)
        {
            GeometryChanged?.Invoke(this, args);
        }

        /// <summary>
        ///     Protected Virtual void function that should define how the geometry entity
        ///     deals with geometry update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void UpdateGeometry(object sender, GeometryChangedHandlerArgs args) { }
    }
}