// Dxflib
// LayerDictionary.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-05-8:53 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Collections.Generic;
using Dxflib.Entities;

namespace Dxflib.AcadEntities
{
    /// <summary>
    ///     A class that holds layer using a dictionary structure
    /// </summary>
    public class LayerDictionary
    {
        // The dictionary base
        private readonly Dictionary<string, Layer> _dictionary;

        /// <summary>
        ///     Constructor that initalizes that LayerDictionary
        /// </summary>
        public LayerDictionary()
        {
            _dictionary = new Dictionary<string, Layer>();
        }

        /// <summary>
        ///     Count from the dictionary base
        /// </summary>
        public int Count => _dictionary.Count;

        /// <summary>
        ///     Adding a new layer to the dictionary
        /// </summary>
        /// <param name="name">The name of the layer</param>
        public void NewLayer(string name)
        {
            if (_dictionary.ContainsKey(name))
                throw new LayerDictionaryException($"The Layer: {name} Already Exists");

            _dictionary.Add(name, new Layer(name));
        }

        /// <summary>
        ///     Returns true or false if the layer is in this dictionary
        /// </summary>
        /// <param name="name">The name of the layer you want to search</param>
        /// <returns>True: If the layer does exits, False: If the layer does not exist</returns>
        public bool ContainsLayer(string name)
        {
            return _dictionary.ContainsKey(name);
        }

        /// <summary>
        ///     Get a layer from the dictionary
        /// </summary>
        /// <param name="name">The name of the layer</param>
        /// <returns>The layer that coresponds with the name that was given</returns>
        public Layer GetLayer(string name)
        {
            if (!_dictionary.ContainsKey(name))
                throw new LayerDictionaryException("The Layer was not found in the dictionary");

            return _dictionary[name];
        }

        public void UpdateDictionary(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                // If the layer does not exist then create it
                if (!ContainsLayer(entity.LayerName))
                    NewLayer(entity.LayerName);

                // If the layer does not already have the entity in it add 
                // that entity to the dictionary
                if (!_dictionary[entity.LayerName].ContainsEntity(entity.Handle))
                    _dictionary[entity.LayerName].AddEntity(entity.Handle, entity);
            }
        }
    }

    /// <summary>
    ///     Layer Exception class, this class handle all layer exceptions
    /// </summary>
    public class LayerDictionaryException : Exception
    {
        public LayerDictionaryException()
        {
        }

        public LayerDictionaryException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}