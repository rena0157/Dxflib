// Dxflib
// Entity.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-05-8:00 AM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;
using Dxflib.Parser;

namespace Dxflib.Entities
{
    public delegate void LayerChangedHandler(object sender, LayerChangedHandlerArgs args);

    /// <summary>
    /// The Entity base class that all entities will be derived
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Public event for layer changes
        /// </summary>
        public event LayerChangedHandler LayerChanged;
        
        /// <summary>
        /// The layer name private backing field 
        /// </summary>
        protected string LayerNameBF;

        /// <summary>
        /// The entity type
        /// </summary>
        public EntityTypes EntityType;

        /// <summary>
        /// The entity's Handel
        /// </summary>
        public string Handle { get; protected set; }

        /// <summary>
        /// The entity's Layer name
        /// </summary>
        public string LayerName
        {
            get => LayerNameBF;
            set
            {
                OnLayerChanged(new LayerChangedHandlerArgs(LayerNameBF, value));
                LayerNameBF = value;
            }
        }

        protected virtual void OnLayerChanged(LayerChangedHandlerArgs args)
        {
            LayerChanged?.Invoke(this, args);
        }
    }

    public class LayerChangedHandlerArgs
    {
        public LayerChangedHandlerArgs(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }

        public string OldName { get; }
        public string NewName { get; }
    }

    /// <summary>
    /// The Entity buffer class that is used only in extraction
    /// </summary>
    public class EntityBuffer
    {
        /// <summary>
        /// The Entity Type
        /// </summary>
        public EntityTypes EntityType;

        /// <summary>
        /// Main Constructor that resets all values
        /// </summary>
        public EntityBuffer()
        {
            Handle = "";
            LayerName = "";
            EntityType = EntityTypes.None;
        }

        public string Handle { get; set; }
        public string LayerName { get; set; }

        public virtual bool Parse(LineChangeHandlerArgs args)
        {
            switch (args.NewCurrentLine)
            {
                case EntityGroupCodes.Handle:
                    Handle = args.NewNextLine;
                    return true;
                // Layer
                case EntityGroupCodes.Layer:
                    LayerName = args.NewNextLine;
                    return true;

                default: return false;
            }
        }
    }

    public static class EntityGroupCodes
    {
        public const string Handle = "  5";
        public const string Layer = "  8";
    }

    public enum EntityTypes
    {
        [Description("LINE")] Line,

        [Description("LWPOLYLINE")] Lwpolyline,

        [Description("Not Set")] None
    }
}