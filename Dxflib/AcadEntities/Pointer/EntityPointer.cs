// Dxflib
// EntityPointer.cs
// 
// ============================================================
// 
// Created: 2018-08-28
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.AcadEntities.Pointer
{
    /// <summary>
    ///     A class that holds reference to an entity
    /// </summary>
    public class EntityPointer<T>
    {
        /// <summary>
        ///     The Entity reference
        /// </summary>
        protected T _entity;

        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// <param name="handle"></param>
        public EntityPointer(string handle)
        {
            Handle = handle;
            EntityType = typeof(T);
        }

        /// <summary>
        ///     The Handle of the Entity
        /// </summary>
        public string Handle { get; protected set; }

        /// <summary>
        ///     The Entity Reference, note that this value should not be null
        /// </summary>
        public virtual T RefEntity
        {
            get
            {
                if ( _entity == null )
                    throw new EntityPointerException("Entity is Null");
                return _entity;
            }
            set
            {
                if ( value == null )
                    throw new EntityPointerException("Entity is Null");
                _entity = value;
            }
        }

        /// <summary>
        ///     The type of the entity the object is pointing to
        /// </summary>
        public Type EntityType { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     The Exception that is thrown if there is an error with the Entity Pointer
    /// </summary>
    public class EntityPointerException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Blank Constructor
        /// </summary>
        public EntityPointerException() { }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor with message
        /// </summary>
        /// <param name="message">The Message to the user</param>
        public EntityPointerException(string message) { Message = message; }

        /// <inheritdoc />
        /// <summary>
        ///     The message
        /// </summary>
        public override string Message { get; }
    }
}