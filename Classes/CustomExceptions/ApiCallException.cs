using System.Net;

namespace Classes.CustomExceptions;


    public class ApiCallException : Exception
    {
        public HttpStatusCode _statusCode { get; }
        public ApiCallException() { }

        public ApiCallException(string message)
            : base(message) { }

        public ApiCallException(string message, HttpStatusCode statusCode)
            : base(statusCode + " - " + message)
        {
            _statusCode = statusCode;
        }
        public ApiCallException(string message, Exception inner)
            : base(message, inner) { }
    }
