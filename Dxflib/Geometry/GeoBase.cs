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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dxflib.Annotations;

namespace Dxflib.Geometry
{
    /// <inheritdoc />
    /// <summary>
    ///     An abstract class that serves two purposes:
    ///     1. To unify all geometric properties
    ///     2. To provide the GeometryChanged Event that
    ///     Occurs when there is a geometry change to alert parent classes
    ///     that they might need to update their geometry
    /// </summary>
    public abstract class GeoBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     The entity type
        /// </summary>
        public GeometryEntityTypes GeometryEntityType { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// Property Changed Event Handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Virtual Function that will update the geometry of a geometric object
        /// </summary>
        /// <param name="command">An optional Command</param>
        protected virtual void UpdateGeometry(string command = "")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}