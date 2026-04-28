using System.Net;

namespace STGenetics.Challenge.Business.Responses
{
    public class RequestHandlerResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public object? Data { get; set; }

        public RequestHandlerResponse(object? data, HttpStatusCode statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }

        public RequestHandlerResponse(string message, HttpStatusCode statusCode)
        {
            Data = message;
            StatusCode = statusCode;
        }
    }
}