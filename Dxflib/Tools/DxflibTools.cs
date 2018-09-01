// Dxflib
// DxflibTools.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.ComponentModel;

namespace Dxflib.Tools
{
    /// <summary>
    ///     General Static Class that holds all kinds of different tools for the
    ///     library
    /// </summary>
    public static class DxflibTools
    {
        /// <summary>
        ///     Get the description from an enumeration
        /// </summary>
        /// <param name="value">The Enumeration that you want to get the description from</param>
        /// <returns>The Description as a string</returns>
        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}