using System;
using System.Collections.Generic;
using Dxflib.Entities;

namespace Dxflib.AcadEntities
{
    public class Layer
    {
        private Dictionary<string, Entity> _entities;

        public Layer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public bool ContainsEntity(string handle)
        {
            return _entities.ContainsKey(handle);
        }

        public void AddEntity(string handle, Entity entity)
        {
            if (_entities.ContainsKey(handle))
                throw new LayerException("This Entity is already a member of this layer");

            _entities.Add(handle, entity);
        }

        public void RemoveEntity(string handle)
        {
            if (!_entities.ContainsKey(handle))
                throw new LayerException("This entity does not exist as a member of this layer");

            _entities.Remove(handle);
        }
    }

    public class LayerException : Exception
    {
        public LayerException()
        {
        }

        public LayerException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}