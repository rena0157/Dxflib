// Dxflib
// Entity.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using Dxflib.AcadEntities.Pointer;

namespace Dxflib.Entities
{
    /// <summary>
    ///     The Delegate for the LayerChanged Event
    /// </summary>
    /// <param name="sender">The Sending Object</param>
    /// <param name="args">The arguments</param>
    public delegate void LayerChangedHandler(object sender, LayerChangedHandlerArgs args);

    /// <summary>
    ///     The Entity base class that all entities will be derived from.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        ///     The layer name private backing field
        /// </summary>
        private string _layerNameBf;

        /// <summary>
        ///     The entity type of this entity
        /// </summary>
        public EntityTypes EntityType;

        /// <summary>
        /// </summary>
        /// <param name="eb"></param>
        protected Entity(EntityBuffer eb)
        {
            EntityType = eb.EntityType;
            _layerNameBf = eb.LayerName;
            Handle = eb.Handle;

            ReferencedEntities = BuildReferenceEntities(eb.EntityReferenceList);
        }

        /// <summary>
        ///     The entity's Handle
        /// </summary>
        public string Handle { get; }

        /// <summary>
        ///     A list of Entity Pointers to referenced entities
        /// </summary>
        /// <remarks>
        ///     A referenced entity is an entity that this entity is referring to
        ///     for example the <see cref="Dxflib.Entities.Hatch.Hatch" /> Entity can reference
        ///     its boundary object.
        /// </remarks>
        public List<EntityPointer<Entity>> ReferencedEntities { get; }

        /// <summary>
        ///     Returns true if <see cref="ReferencedEntities" /> count > 0
        /// </summary>
        public bool HasReferencedEntities => ReferencedEntities.Count > 0;

        /// <summary>
        ///     The entity's Layer name
        /// </summary>
        /// <remarks>
        ///     Note that a <see cref="LayerChanged" /> event
        ///     will be fired off if this property is changed.
        /// </remarks>
        public string LayerName
        {
            get => _layerNameBf;
            set
            {
                OnLayerChanged(new LayerChangedHandlerArgs(_layerNameBf, value));
                _layerNameBf = value;
            }
        }

        /// <summary>
        ///     Build All of the entityReferences
        /// </summary>
        /// <param name="entityHandlesList">A List of handles from the Entity's Buffer</param>
        /// <returns>A list of entity Pointers</returns>
        private static List<EntityPointer<Entity>> BuildReferenceEntities(List<string> entityHandlesList)
        {
            if ( entityHandlesList == null )
                return new List<EntityPointer<Entity>>();

            var returnList = new List<EntityPointer<Entity>>(entityHandlesList.Capacity);

            foreach ( var handle in entityHandlesList )
                returnList.Add(new EntityPointer<Entity>(handle));

            return returnList;
        }

        /// <summary>
        ///     The Base Function does nothing
        /// </summary>
        public virtual void UpdateReferencedEntities() { }

        /// <summary>
        ///     Public event for <see cref="LayerName" /> changes
        /// </summary>
        public event LayerChangedHandler LayerChanged;


        /// <summary>
        ///     The OnLayerChanged Function that invokes all of the subscribing methods
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnLayerChanged(LayerChangedHandlerArgs args) { LayerChanged?.Invoke(this, args); }
    }
}