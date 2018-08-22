// Dxflib
// Entity.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-07-11:02 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.ComponentModel;
using Dxflib.IO;

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
        ///     The entity type of this entity
        /// </summary>
        public EntityTypes EntityType;

        /// <summary>
        ///     The layer name private backing field
        /// </summary>
        protected string LayerNameBf;

        /// <summary>
        ///     The entity's Handle
        /// </summary>
        public string Handle { get; protected set; }

        /// <summary>
        ///     The entity's Layer name
        /// </summary>
        /// <remarks>
        ///     Note that a <see cref="LayerChanged"/> event
        ///     will be fired off if this property is changed.
        /// </remarks>
        public string LayerName
        {
            get => LayerNameBf;
            set
            {
                OnLayerChanged(new LayerChangedHandlerArgs(LayerNameBf, value));
                LayerNameBf = value;
            }
        }

        /// <summary>
        ///     Public event for <see cref="LayerName"/> changes
        /// </summary>
        public event LayerChangedHandler LayerChanged;

        /// <summary>
        ///     The OnLayerChanged Function that invokes all of the subscribing methods
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnLayerChanged(LayerChangedHandlerArgs args)
        {
            LayerChanged?.Invoke(this, args);
        }
    }

    /// <summary>
    ///     Handler Arguments for the LayerChanged Event
    /// </summary>
    public class LayerChangedHandlerArgs
    {
        /// <summary>
        ///     The Main Constructor for the layer changed event
        /// </summary>
        /// <param name="oldName">The Old Layer Name</param>
        /// <param name="newName">The New Layer Name</param>
        public LayerChangedHandlerArgs(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }

        /// <summary>
        ///     The Old Layer Name
        /// </summary>
        public string OldName { get; }

        /// <summary>
        ///     The New Layer Name
        /// </summary>
        public string NewName { get; }
    }

    /// <summary>
    ///     The Entity buffer class that is used only in extraction
    /// </summary>
    public class EntityBuffer
    {
        /// <summary>
        ///     The Entity Type
        /// </summary>
        public EntityTypes EntityType;

        /// <summary>
        ///     Main Constructor that resets all values
        /// </summary>
        protected EntityBuffer()
        {
            Handle = "";
            LayerName = "";
            EntityType = EntityTypes.None;
        }

        /// <summary>
        ///     The Entity's Handle
        /// </summary>
        public string Handle { get; set; }

        /// <summary>
        ///     The Entity's Layer Name
        /// </summary>
        public string LayerName { get; set; }

        /// <summary>
        ///     The Parse Virtual Function that is to be overriden by
        ///     each entity that is to be extracted. This function also,
        ///     Parses global entity properties such as handle or <see cref="LayerName"/>.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>True if the parse was successful</returns>
        public virtual bool Parse(TaggedDataList list, int index)
        {
            var currentData = list.GetPair(index);
            switch ( currentData.GroupCode )
            {
                case GroupCodesBase.Handle:
                    Handle = currentData.Value;
                    return true;
                case GroupCodesBase.LayerName:
                    LayerName = currentData.Value;
                    return true;
                default:
                    return false;
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     The Entity Exception Class
    /// </summary>
    public class EntityException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Blank Constructor
        /// </summary>
        public EntityException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor with a message
        /// </summary>
        /// <param name="message"></param>
        public EntityException(string message)
        {
            Message = message;
        }

        /// <inheritdoc />
        /// <summary>
        ///     The Message override required to set the message property
        /// </summary>
        public override string Message { get; }
    }

    /// <summary>
    ///     All of the different entity types that are currently supported for extraction
    /// </summary>
    public enum EntityTypes
    {
        /// <summary>
        /// <see cref="Dxflib.Entities.Line"/>
        /// </summary>
        [Description("LINE")] Line,

        /// <summary>
        /// <see cref="Dxflib.Entities.LwPolyLine"/>
        /// </summary>
        [Description("LWPOLYLINE")] Lwpolyline,

        /// <summary>
        /// No specific Entity
        /// </summary>
        [Description("Not Set")] None
    }
}