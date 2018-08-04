using System;

namespace Dxflib.DxfStream
{
    public class DxfStreamException : Exception
    {
        public DxfStreamException()
        {
        }

        public DxfStreamException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}