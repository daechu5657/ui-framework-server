using System.Net;

namespace UiFrameworkServer
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(
            int? errorCode = null,
            string? message = null,
            Exception? innerException = null
        )
            : base(HttpStatusCode.BadRequest, errorCode, message, innerException) { }
    }
}
