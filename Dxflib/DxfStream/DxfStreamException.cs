using System;

namespace Dxflib.DxfStream
{
    /// <inheritdoc />
    /// <summary>
    /// DxfStream Exception class
    /// </summary>
    public class DxfStreamException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Blank Constructor
        /// </summary>
        public DxfStreamException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor with message
        /// </summary>
        /// <param name="message">The Message</param>
        public DxfStreamException(string message)
        {
            Message = message;
        }

        /// <inheritdoc />
        /// <summary>
        /// The Message
        /// </summary>
        public override string Message { get; }
    }
}