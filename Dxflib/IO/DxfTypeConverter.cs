// Dxflib
// DxfTypeConverter.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.IO
{
    /// <summary>
    ///     This public static class has only one purpose, which is to return hold the
    ///     <see cref="ConvertType" /> Function.
    /// </summary>
    public static class DxfTypeConverter
    {
        /// <summary>
        ///     The Convert Type Function is used to get the correct AutoCAD
        ///     Value type. This is consistent will the AutoCAD 2019 Standard.
        /// </summary>
        /// <exception cref="InvalidCastException">
        ///     Throws when a group code is not recognized.
        /// </exception>
        /// <param name="groupCode">The Group Code String</param>
        /// <returns>
        ///     A <see cref="Type" /> that corresponds with the AutoCAD
        ///     Type
        /// </returns>
        public static Type ConvertType(string groupCode)
        {
            // The Group code first has to be converted to an
            // int to be used in the switch statement with a range
            var gc = int.Parse(groupCode);

            // The master switch statement. Not that all of the statements
            // Use ranges which is only compatible with C# 7+ I believe.
            switch ( gc )
            {
                case int _ when gc >= 0 && gc <= 9:
                    return typeof(string);

                case int _ when gc >= 10 && gc <= 59:
                    return typeof(double);

                case int _ when gc >= 60 && gc <= 79:
                    return typeof(short);

                case int _ when gc >= 60 && gc <= 99:
                    return typeof(int);

                case int _ when gc == 100 || gc == 102 || gc == 105:
                    return typeof(string);

                case int _ when gc >= 110 && gc <= 149:
                    return typeof(double);

                case int _ when gc >= 160 && gc <= 169:
                    return typeof(long);

                case int _ when gc >= 170 && gc <= 179:
                    return typeof(short);

                case int _ when gc >= 210 && gc <= 239:
                    return typeof(double);

                case int _ when gc >= 270 && gc <= 289:
                    return typeof(short);

                case int _ when gc >= 290 && gc <= 299:
                    return typeof(bool);

                case int _ when gc >= 300 && gc <= 369:
                    return typeof(string);

                case int _ when gc >= 370 && gc <= 389:
                    return typeof(short);

                case int _ when gc >= 390 && gc <= 399:
                    return typeof(string);

                case int _ when gc >= 400 && gc <= 409:
                    return typeof(short);

                case int _ when gc >= 410 && gc <= 419:
                    return typeof(string);

                case int _ when gc >= 420 && gc <= 429:
                    return typeof(int);

                case int _ when gc >= 430 && gc <= 439:
                    return typeof(string);

                case int _ when gc >= 440 && gc <= 449:
                    return typeof(int);

                case int _ when gc >= 450 && gc <= 459:
                    return typeof(long);

                case int _ when gc >= 460 && gc <= 469:
                    return typeof(double);

                case int _ when gc >= 470 && gc <= 481:
                    return typeof(string);

                case int _ when gc == 999:
                    return typeof(string);

                case int _ when gc >= 1000 && gc <= 1009:
                    return typeof(string);

                case int _ when gc >= 1010 && gc <= 1059:
                    return typeof(double);

                case int _ when gc >= 1060 && gc <= 1070:
                    return typeof(short);

                case int _ when gc == 1071:
                    return typeof(int);

                // If the Group Code is not recognized then throw an exception
                default:
                    throw new InvalidCastException(
                        $"The GroupCode '{groupCode}', was not recognized as a valid group code");
            }
        }
    }
}