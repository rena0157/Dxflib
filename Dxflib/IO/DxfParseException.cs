// Dxflib
// DxfParseException.cs
// 
// ============================================================
// 
// Created: 2018-08-27
// Last Updated: 2018-08-27-9:07 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.IO
{
    /// <inheritdoc />
    /// <summary>
    ///     This Exception should be used if there is an unexpected situation in
    ///     the parsing of a dxf file
    /// </summary>
    public class DxfParseException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     A Blank Constructor
        /// </summary>
        public DxfParseException() { }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor with a message
        /// </summary>
        /// <param name="message"></param>
        public DxfParseException(string message) { Message = message; }

        /// <inheritdoc />
        /// <summary>
        ///     Message to the Developer or user
        /// </summary>
        public override string Message { get; }
    }
}