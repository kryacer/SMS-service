using System.Net;
using System.Runtime.Serialization;

namespace SMS_Service.BLL.Exceptions
{
	[Serializable]
	public class FDXException : Exception
	{
        public HttpStatusCode StatusCode { get; private set; }

        public string ErrorCode { get; private set; }

        public FDXException()
        {
        }

        public FDXException(HttpStatusCode statusCode, string errorCode)
            : this(statusCode, errorCode, null, null)
        {
        }

        public FDXException(HttpStatusCode statusCode, string errorCode, string message)
            : this(statusCode, errorCode, message, null)
        {
        }

        public FDXException(HttpStatusCode statusCode, string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        protected FDXException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                return;
            }

            StatusCode = (HttpStatusCode)info.GetValue("StatusCode", typeof(HttpStatusCode));
            ErrorCode = (string)info.GetValue("ErrorCode", typeof(string));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                return;
            }

            base.GetObjectData(info, context);
            info.AddValue("StatusCode", StatusCode);
            info.AddValue("ErrorCode", ErrorCode);
        }
    }
}
