// Dxflib
// Entity.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-04-5:25 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose:

using System.ComponentModel;
using Dxflib.Parser;

namespace Dxflib.Entities
{
    public abstract class Entity
    {
        public EntityTypes EntityType;
        public string Handle { get; protected set; }
        public string LayerName { get; protected set; }
    }

    public class EntityBuffer
    {
        public EntityTypes EntityType;

        public EntityBuffer()
        {
            handle = "";
            LayerName = "";
            EntityType = EntityTypes.None;
        }

        public string handle { get; set; }
        public string LayerName { get; set; }

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
        public const string Handle = " 5";
        public const string Layer = " 8";
    }

    public enum EntityTypes
    {
        [Description("LINE")] Line,

        [Description("LWPOLYLINE")] Lwpolyline,

        [Description("Not Set")] None
    }
}