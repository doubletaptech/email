using System;
using System.Runtime.Serialization;

namespace DoubleTap.Email
{
    [Serializable]
    public class EmailBuilderException : Exception
    {
        public EmailBuilderException()
        {
        }

        protected EmailBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EmailBuilderException(string message) : base(message)
        {
        }

        public EmailBuilderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}