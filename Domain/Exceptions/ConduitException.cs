using System.Net;

namespace Domain.Exceptions;

public class ConduitException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}