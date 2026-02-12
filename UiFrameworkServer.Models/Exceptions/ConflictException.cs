using System.Net;

namespace UiFrameworkServer
{
    public class ConflictException : HttpException
    {
        public ConflictException(
            int? errorCode = null,
            string? message = null,
            Exception? innerException = null
        )
            : base(HttpStatusCode.Conflict, errorCode, message, innerException) { }
    }
}
