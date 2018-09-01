// Dxflib
// LayerDictionary.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-1:09 PM
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
    ///     A class that holds layers using a dictionary structure with the layer
    ///     names being used as the keys.
    /// </summary>
    public class LayerDictionary
    {
        // The dictionary base
        private readonly Dictionary<string, Layer> _dictionary;

        /// <summary>
        ///     Constructor that initializes the Layer Dictionary to an
        ///     empty <see cref="Dictionary{TKey,TValue}" />
        /// </summary>
        public LayerDictionary() { _dictionary = new Dictionary<string, Layer>(); }

        /// <summary>
        ///     The number of objects in the dictionary
        /// </summary>
        public int Count => _dictionary.Count;

        /// <summary>
        ///     Returns all layers as a <list type="Layer"></list>
        /// </summary>
        public List<Layer> GetAllLayers => _dictionary.Values.ToList();

        /// <summary>
        ///     Adding a new layer to the dictionary
        /// </summary>
        /// <exception cref="LayerException">
        ///     Thrown when the <paramref name="name" /> is already a member of the dictionary
        /// </exception>
        /// <param name="name">The name of the layer</param>
        public void NewLayer(string name)
        {
            if ( _dictionary.ContainsKey(name) )
                throw new LayerDictionaryException($"The Layer: {name} Already Exists");

            _dictionary.Add(name, new Layer(name));
        }

        /// <summary>
        ///     Returns true or false if the layer is in this dictionary
        /// </summary>
        /// <param name="name">The name of the layer you want to search</param>
        /// <returns>True: If the layer does exits, False: If the layer does not exist</returns>
        public bool ContainsLayer(string name) { return _dictionary.ContainsKey(name); }

        /// <summary>
        ///     Get a <see cref="Layer" /> from the dictionary by its name
        /// </summary>
        /// <param name="name">The name of the layer</param>
        /// <returns>
        ///     The layer that corresponds with the
        ///     <paramref name="name" /> that was given
        /// </returns>
        public Layer GetLayer(string name)
        {
            if ( !_dictionary.ContainsKey(name) )
                throw new LayerDictionaryException("The Layer was not found in the dictionary");

            return _dictionary[name];
        }

        /// <summary>
        ///     Updates the dictionary.
        ///     If a layer does not exist then it adds it to the dictionary and
        ///     adds entities to their respective layers.
        /// </summary>
        /// <param name="entities">Entities that you want the dictionary to have</param>
        public void UpdateDictionary(IEnumerable<Entity> entities)
        {
            // Add entities to their layers
            foreach ( var entity in entities )
            {
                // If the layer does not exist then create it
                if ( !ContainsLayer(entity.LayerName) )
                    NewLayer(entity.LayerName);

                // If the layer does not already have the entity in it add 
                // that entity to the dictionary
                if ( !_dictionary[entity.LayerName].ContainsEntity(entity.Handle) )
                {
                    _dictionary[entity.LayerName].AddEntity(entity.Handle, entity);
                    entity.LayerChanged += EntityOnLayerChanged;
                }
            }

            // Iterate through all layers and entities\
            // remove entities from layers if their layer name does not
            // match the layer that the dictionary has them on.
            foreach ( var layer in _dictionary.Values.ToList() )
            foreach ( var entity in layer.GetAllEntities() )
                // If the layer-name does not match the current layer
                // then clean up that layer
                if ( entity.LayerName != layer.Name )
                    _dictionary[layer.Name].RemoveEntity(entity.Handle);
        }

        /// <summary>
        ///     Entity on layer changed is called when ever a layerName is changed on
        ///     an entity
        /// </summary>
        /// <param name="sender">The Sender and entity</param>
        /// <param name="args">Arguments</param>
        private void EntityOnLayerChanged(object sender, LayerChangedHandlerArgs args)
        {
            // Delete the old reference
            _dictionary[args.OldName].RemoveEntity(( (Entity) sender ).Handle);

            // If the layer does not already exist then create it
            if ( !_dictionary.ContainsKey(args.NewName) )
                _dictionary.Add(args.NewName, new Layer(args.NewName));

            // Add the entity to the new layer name
            _dictionary[args.NewName].AddEntity(( (Entity) sender ).Handle, (Entity) sender);
        }

        /// <summary>
        ///     Remove a layer by its <paramref name="name" />
        /// </summary>
        /// <param name="name">The name of the layer that is to be removed</param>
        /// <returns>True: if Successful</returns>
        public bool RemoveLayer(string name) { return _dictionary.Remove(name); }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Layer Exception class, this class handle all layer exceptions
    /// </summary>
    public class LayerDictionaryException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Blank Constructor
        /// </summary>
        public LayerDictionaryException() { }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor with a message
        /// </summary>
        /// <param name="message">The Message</param>
        public LayerDictionaryException(string message) { Message = message; }

        /// <inheritdoc />
        /// <summary>
        ///     Message to send to the user
        /// </summary>
        public override string Message { get; }
    }
}