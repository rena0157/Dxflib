// Dxflib
// Layer.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-07-11:09 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Dxflib.Entities;

namespace Dxflib.AcadEntities
{
    /// <summary>
    ///     The Layer Class
    /// </summary>
    public class Layer
    {
        // The dictionary backing field
        private readonly Dictionary<string, Entity> _entities;


        /// <summary>
        ///     Layer Constructor
        /// </summary>
        /// <param name="name">The Layer Name</param>
        public Layer(string name)
        {
            Name = name;
            _entities = new Dictionary<string, Entity>();
        }

        /// <summary>
        ///     The Layer's Name
        /// </summary>
        public string Name { get; set; }

        public int Count => _entities.Count;

        /// <summary>
        ///     Returns true if the layer contains the entity
        ///     with the provided handle
        /// </summary>
        /// <param name="handle">Handle of the entity</param>
        /// <returns>
        ///     True: The entity is contained, False: the entity is not
        ///     contained
        /// </returns>
        public bool ContainsEntity(string handle)
        {
            return _entities.ContainsKey(handle);
        }

        /// <summary>
        ///     Adds an entity to the dictionary
        /// </summary>
        /// <param name="handle">The entity's handle</param>
        /// <param name="entity">The entity reference</param>
        public void AddEntity(string handle, Entity entity)
        {
            if (_entities.ContainsKey(handle))
                throw new LayerException("This Entity is already a member of this layer");

            _entities.Add(handle, entity);
        }

        /// <summary>
        ///     Removes the entity from the dictionary
        /// </summary>
        /// <param name="handle"></param>
        public void RemoveEntity(string handle)
        {
            if (!_entities.ContainsKey(handle))
                throw new LayerException("This entity does not exist as a member of this layer");

            _entities.Remove(handle);
        }

        /// <summary>
        ///     Get all of the entities on the layer
        /// </summary>
        /// <returns>a list of all of the entities on the layer</returns>
        public List<Entity> GetAllEntities()
        {
            return _entities.Values.ToList();
        }

        /// <summary>
        ///     Gets all of the handles from the dictionary
        /// </summary>
        /// <returns>A list of handles</returns>
        public List<string> GetAllHandles()
        {
            return _entities.Keys.ToList();
        }

        /// <summary>
        ///     Gets a layer from it's handle
        /// </summary>
        /// <param name="handle">The handle of the entity</param>
        /// <returns>The entity</returns>
        public Entity GetEntity(string handle)
        {
            if (!_entities.ContainsKey(handle))
                throw new LayerException($"Layer does not contain: {handle}");

            return _entities[handle];
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Layer Exception Class
    /// </summary>
    public class LayerException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Barebones constructor
        /// </summary>
        public LayerException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Throw an exception with a message
        /// </summary>
        /// <param name="message">The Message</param>
        public LayerException(string message)
        {
            Message = message;
        }

        /// <inheritdoc />
        /// <summary>
        ///     The message from the exception
        /// </summary>
        public override string Message { get; }
    }
}