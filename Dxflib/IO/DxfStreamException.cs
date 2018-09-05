// Dxflib
// DxfStreamException.cs
// 
// ============================================================
// 
// Created: 2018-08-03
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.IO
{
    /// <inheritdoc />
    /// <summary>
    ///     DxfStream Exception class
    /// </summary>
    public class DxfStreamException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Blank Constructor
        /// </summary>
        public DxfStreamException() { }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor with message
        /// </summary>
        /// <param name="message">The Message</param>
        public DxfStreamException(string message) { Message = message; }

        /// <inheritdoc />
        /// <summary>
        ///     The Message
        /// </summary>
        public override string Message { get; }
    }
}