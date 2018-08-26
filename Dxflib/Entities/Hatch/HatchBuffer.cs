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
using Dxflib.IO;

namespace Dxflib.Entities.Hatch
{
    /// <inheritdoc />
    /// <summary>
    ///     The Hatch Buffer class
    /// </summary>
    public class HatchBuffer : EntityBuffer
    {
        public HatchBuffer() { HatchPatternName = string.Empty; }

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
                    default:
                        continue;
                }
            }

            return base.Parse(list, index);
        }
    }
}