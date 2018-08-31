// Dxflib
// EntityCollection.cs
// 
// ============================================================
// 
// Created: 2018-08-30
// Last Updated: 2018-08-30-8:42 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dxflib.Entities;
using Dxflib.IO;

namespace Dxflib.AcadEntities
{
    /// <inheritdoc />
    /// <summary>
    ///     A class that inherits from <see cref="IDictionary{TKey,TValue}" />
    ///     where TKey is a string and TValue is an entity. This collection is used
    ///     to store <see cref="Entity" /> objects by their <see cref="Entity.Handle" />
    /// </summary>
    public class EntityCollection : IDictionary<string, Entity>
    {
        // The backing Dictionary
        private readonly Dictionary<string, Entity> _dictionary;

        /// <summary>
        ///     Constructor for the EntityCollection Type.
        ///     This constructor allocate the backing dictionary
        ///     with the handles of <paramref name="entities" />.
        ///     It will however not link the entities
        /// </summary>
        public EntityCollection(IReadOnlyCollection<Entity> entities)
        {
            // Throw an exception if the entities list is null
            if ( entities == null )
                throw new DxfParseException("Entities cannot be Null");

            // Initialize the backing field and allocate the handles of the entities
            _dictionary = new Dictionary<string, Entity>();
            foreach ( var entity in entities )
                _dictionary.Add(entity.Handle, entity);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, Entity>> GetEnumerator() { return _dictionary.GetEnumerator(); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() { return _dictionary.GetEnumerator(); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, Entity> item) { _dictionary.Add(item.Key, item.Value); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public void Clear() { _dictionary.Clear(); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<string, Entity> item) { return _dictionary.Contains(item); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<string, Entity>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, Entity> item) { return _dictionary.Remove(item.Key); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="handle">The Entity Handle</param>
        /// <returns></returns>
        public bool Remove(string handle) { return _dictionary.Remove(handle); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public int Count => _dictionary.Count;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public bool IsReadOnly => false;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="handle">The Entity Handle</param>
        /// <param name="entity">The Entity</param>
        public void Add(string handle, Entity entity) { _dictionary.Add(handle, entity); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public bool ContainsKey(string handle) { return _dictionary.ContainsKey(handle); }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool TryGetValue(string handle, out Entity entity)
        {
            return _dictionary.TryGetValue(handle, out entity);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Entity this[string key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public ICollection<string> Keys => _dictionary.Keys;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public ICollection<Entity> Values => _dictionary.Values;
    }
}