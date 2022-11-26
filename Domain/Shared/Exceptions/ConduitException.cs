using System.Net;

namespace Domain.Shared.Exceptions;

public class ConduitException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}