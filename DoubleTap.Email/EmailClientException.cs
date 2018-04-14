using System;
using System.Runtime.Serialization;

namespace DoubleTap.Email
{
    [Serializable]
    public class EmailClientException : Exception
    {
        public EmailClientException()
        {
        }

        protected EmailClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EmailClientException(string message) : base(message)
        {
        }

        public EmailClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}