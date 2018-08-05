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
    /// <summary>
    /// The Entity base class that all entities will be derived
    /// </summary>
    public abstract class Entity
    {
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
        public string LayerName { get; protected set; }
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
            handle = "";
            LayerName = "";
            EntityType = EntityTypes.None;
        }

        public string handle { get; private set; }
        public string LayerName { get; private set; }

        public virtual bool Parse(LineChangeHandlerArgs args)
        {
            switch (args.NewCurrentLine)
            {
                case EntityGroupCodes.Handle:
                    handle = args.NewNextLine;
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