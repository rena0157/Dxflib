// Dxflib
// EntityParser.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-08-04-5:16 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose:

using System;
using Dxflib.Entities;

namespace Dxflib.Parser
{
    public static class EntityParser
    {
        // Extracton Properties
        private const string EndMarker = "  0";
        //Line
        private const string LineStartMarker = "LINE";

        /// <summary>
        /// Entity Parse Function that takes the mainParser and linechanged Args and
        /// goes through the decision tree of what to parse and what to extract.
        /// </summary>
        /// <param name="mainParser">The main parser class</param>
        /// <param name="args">LineChangedHandlerArgs class</param>
        public static void Parse(DxfFileMainParser mainParser, LineChangeHandlerArgs args)
        {
            // Entity Selection Branch
            switch (args.NewCurrentLine)
            {
                // Line
                case LineStartMarker:
                    mainParser.CurrentEntityForExtraction = EntityTypes.Line;
                    mainParser.LineBuf = new LineBuffer();
                    break;
                // End Marker
                case EndMarker:
                    BuildEntity(mainParser);
                    mainParser.CurrentEntityForExtraction = EntityTypes.None;
                    break;
            }

            // Entity Parsing Branch
            switch (mainParser.CurrentEntityForExtraction)
            {
                // Line
                case EntityTypes.Line:
                    mainParser.LineBuf.Parse(args);
                    break;
                // Lwpolyline
                case EntityTypes.Lwpolyline:
                    break;
                // None
                case EntityTypes.None:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void BuildEntity(DxfFileMainParser mainParser)
        {
            switch (mainParser.CurrentEntityForExtraction)
            {
                case EntityTypes.Line:
                    mainParser.ThisFile.Entities.Add(new Line(mainParser.LineBuf));
                    break;
                case EntityTypes.Lwpolyline:
                    break;
                case EntityTypes.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}