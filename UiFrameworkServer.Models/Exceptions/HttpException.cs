using System.Net;

namespace UiFrameworkServer
{
    public abstract class HttpException : Exception
    {
        public const string ErrorCodeHeaderName = "C-ERROR-CODE";

        public HttpStatusCode StatusCode { get; set; }

        public int? ErrorCode { get; set; }

        public HttpException(
            HttpStatusCode statusCode,
            int? errorCode = null,
            string? message = null,
            Exception? innerException = null
        )
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
