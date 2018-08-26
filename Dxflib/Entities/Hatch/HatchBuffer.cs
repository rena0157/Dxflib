// Dxflib
// HatchBuffer.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-08-26-5:00 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.Geometry;
using Dxflib.IO;

namespace Dxflib.Entities.Hatch
{
    /// <inheritdoc />
    /// <summary>
    ///     The Hatch Buffer class
    /// </summary>
    public class HatchBuffer : EntityBuffer
    {
        /// <inheritdoc />
        /// <summary>
        /// The Default Constructor that sets all values to their defaults
        /// </summary>
        public HatchBuffer()
        {
            HatchPatternName = string.Empty;
            SolidFillFlag = false;
            AssociativityFlag = false;
            NumberOfLoops = 0;
            HatchStyle = HatchStyles.Normal;
            PatternType = HatchPatternType.Predefined;
            PatternAngle = 0.0;
            PatternScale = 0.0;
            NumberOfPatternDefLines = 0;
            Boundary = new GeoPolyline();
        }

        /// <summary>
        ///     <see cref="HatchCodes.HatchPatternName" />
        /// </summary>
        public string HatchPatternName { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.SolidFillFlag" />
        /// </summary>
        public bool SolidFillFlag { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.AssociativityFlag" />
        /// </summary>
        public bool AssociativityFlag { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.NumberOfBoundaryLoops" />
        /// </summary>
        public int NumberOfLoops { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.HatchStyle" />
        /// </summary>
        public HatchStyles HatchStyle { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.HatchPatternType" />
        /// </summary>
        public HatchPatternType PatternType { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.HatchPatternAngle" />
        /// </summary>
        public double PatternAngle { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.NumberOfPatternDefLines" />
        /// </summary>
        public int NumberOfPatternDefLines { get; private set; }

        /// <summary>
        ///     <see cref="HatchCodes.HatchPatternScale" />
        /// </summary>
        public double PatternScale { get; private set; }

        /// <summary>
        /// <see cref="HatchCodes.NumberOfEdgesInBoundary"/>
        /// </summary>
        public int BoundaryEdgesCount { get; private set; }

        /// <summary>
        /// The Boundary of the Hatch
        /// </summary>
        public GeoPolyline Boundary { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Parsing Function for the <see cref="Hatch" /> entity
        /// </summary>
        /// <param name="list">The Tagged Data List</param>
        /// <param name="index">The current index of the list</param>
        /// <returns></returns>
        public override bool Parse(TaggedDataList list, int index)
        {
            EntityType = EntityTypes.Hatch;
            for ( var currentIndex = index + 1;
                currentIndex < list.Length;
                ++currentIndex )
            {
                var currentData = list.GetPair(currentIndex);

                if ( currentData.GroupCode == GroupCodesBase.EntityType )
                    break;

                if ( base.Parse(list, currentIndex) )
                    continue;

                switch ( currentData.GroupCode )
                {
                    // Hatch Pattern Name
                    case HatchCodes.HatchPatternName:
                        HatchPatternName = currentData.Value;
                        continue;
                    // Solid Fill Flag
                    case HatchCodes.SolidFillFlag:
                        SolidFillFlag = currentData.Value.Contains("1");
                        continue;
                    // Associativity Flag
                    case HatchCodes.AssociativityFlag:
                        AssociativityFlag = currentData.Value.Contains("1");
                        continue;
                    // Number of Boundary Paths (Loops)
                    case HatchCodes.NumberOfBoundaryLoops:
                        NumberOfLoops = int.Parse(currentData.Value);
                        continue;
                    // Hatch Style
                    case HatchCodes.HatchStyle:
                        switch ( (HatchStyles) int.Parse(currentData.Value) )
                        {
                            case HatchStyles.Normal:
                                HatchStyle = HatchStyles.Normal;
                                break;
                            case HatchStyles.Outer:
                                HatchStyle = HatchStyles.Outer;
                                break;
                            case HatchStyles.Ignore:
                                HatchStyle = HatchStyles.Ignore;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        continue;
                    // Pattern Type
                    case HatchCodes.HatchPatternType:
                        switch ( (HatchPatternType) int.Parse(currentData.Value) )
                        {
                            case HatchPatternType.UserDefined:
                                PatternType = HatchPatternType.UserDefined;
                                break;
                            case HatchPatternType.Predefined:
                                PatternType = HatchPatternType.Predefined;
                                break;
                            case HatchPatternType.Custom:
                                PatternType = HatchPatternType.Custom;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        continue;
                    // Pattern Angle
                    case HatchCodes.HatchPatternAngle:
                        PatternAngle = double.Parse(currentData.Value);
                        continue;
                    // Pattern Scale
                    case HatchCodes.HatchPatternScale:
                        PatternScale = double.Parse(currentData.Value);
                        continue;
                    // Parsing the Boundary Data
                    case HatchCodes.NumberOfEdgesInBoundary:
                        BoundaryEdgesCount = int.Parse(currentData.Value);
                        Boundary = ParseBoundary(list, ref currentIndex);
                        continue;
                    default:
                        continue;
                }
            }
            return true;
        }

        /// <summary>
        /// Parsing the Boundary Edge from edge Data
        /// </summary>
        /// <param name="list">The <see cref="TaggedDataList"/> list</param>
        /// <param name="index">The current index</param>
        /// <returns>A GeoPolyline</returns>
        private static GeoPolyline ParseBoundary(TaggedDataList list, ref int index)
        {
            var geoPolyline = new GeoPolyline(); // The Geo Polyline to be returned
            for ( ; index < list.Length; ++index )
            {
                var currentData = list.GetPair(index);

                switch ( currentData.GroupCode )
                {
                    // Edge Types
                    case HatchCodes.EdgeType:
                        switch ( (EdgeTypes)int.Parse(currentData.Value) )
                        {
                            case EdgeTypes.Line:
                                geoPolyline.Add(ParseLineEdge(list, ref index));
                                break;
                            case EdgeTypes.CircularArc:
                                geoPolyline.Add(ParseCircularArcEdge(list, ref index));
                                break;
                            case EdgeTypes.EllipticalArc:
                                break;
                            case EdgeTypes.Spline:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        continue;

                    // When to exit the loop
                    case HatchCodes.SourceObjectsCount:
                        --index;
                        return geoPolyline;

                    default:
                        continue;
                }
            }
            // This area of code should not be reachable
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Parse a geoline from a hatch boundary
        /// </summary>
        /// <param name="list">The <see cref="TaggedDataList"/> list</param>
        /// <param name="index">The current index</param>
        /// <returns>The GeoLine Parsed</returns>
        private static GeoLine ParseLineEdge(TaggedDataList list, ref int index)
        {
            // All of the variables for the GeoLine
            var x0 = 0.0;
            var x1 = 0.0;
            var y0 = 0.0;
            // Iterate through the list
            for ( ; index < list.Length; ++index )
            {
                var currentData = list.GetPair(index);

                switch ( currentData.GroupCode )
                {
                    case GroupCodesBase.XPoint:
                        x0 = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.XPointEnd:
                        x1 = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.YPoint:
                        y0 = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.YPointEnd:
                        var y1 = double.Parse(currentData.Value);
                        // This is the last data point so return
                        return new GeoLine(new Vertex(x0, y0), new Vertex(x1, y1));
                    default:
                        continue;
                }
            }
            // this part of the code should never be reached
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Parse a <see cref="GeoArc"/> from the boundary data in the Hatch
        /// </summary>
        /// <param name="list">The Tagged Data List</param>
        /// <param name="index">The current index</param>
        /// <returns></returns>
        private static GeoArc ParseCircularArcEdge(TaggedDataList list, ref int index)
        {
            var cpX = 0.0;
            var cpY = 0.0;
            var radius = 0.0;
            var startAngle = 0.0;
            for ( ; index < list.Length; ++index )
            {
                var currentData = list.GetPair(index);
                switch ( currentData.GroupCode )
                {
                    case GroupCodesBase.XPoint:
                        cpX = double.Parse(currentData.Value);
                        continue;
                    case GroupCodesBase.YPoint:
                        cpY = double.Parse(currentData.Value);
                        continue;
                    case CircularArcCodes.Radius:
                        radius = double.Parse(currentData.Value);
                        continue;
                    case CircularArcCodes.StartAngle:
                        startAngle = double.Parse(currentData.Value);
                        continue;
                    case CircularArcCodes.EndAngle:
                        var endAngle = double.Parse(currentData.Value);
                        return new GeoArc(new Vertex(cpX, cpY), startAngle, endAngle, radius);
                    default:
                        continue;
                }
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}