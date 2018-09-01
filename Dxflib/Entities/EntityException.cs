// Dxflib
// EntityException.cs
// 
// ============================================================
// 
// Created: 2018-08-30
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     The Entity Exception Class
    /// </summary>
    public class EntityException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Blank Constructor
        /// </summary>
        public EntityException() { }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor with a message
        /// </summary>
        /// <param name="message"></param>
        public EntityException(string message) { Message = message; }

        /// <inheritdoc />
        /// <summary>
        ///     The Message override required to set the message property
        /// </summary>
        public override string Message { get; }
    }
}