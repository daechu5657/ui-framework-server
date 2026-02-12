using System.Net;

namespace UiFrameworkServer
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(
            int? errorCode = null,
            string? message = null,
            Exception? innerException = null
        )
            : base(HttpStatusCode.NotFound, errorCode, message, innerException) { }
    }
}
