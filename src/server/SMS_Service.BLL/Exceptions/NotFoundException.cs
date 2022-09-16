using System.Net;
using System.Runtime.Serialization;

namespace SMS_Service.BLL.Exceptions
{
	[Serializable]
	internal class NotFoundException : FDXException
	{
        public NotFoundException()
               : base(HttpStatusCode.NotFound, ErrorCodes.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(HttpStatusCode.NotFound, ErrorCodes.NotFound, message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(HttpStatusCode.NotFound, ErrorCodes.NotFound, message, innerException)
        {

        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
