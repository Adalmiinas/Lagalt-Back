namespace Lagalt
{
  public class ApiExceptions
  {
    public int StatusCode { get; }
    public string Message { get; }
    public string Details { get; }
    public ApiExceptions(int statusCode, string message, string details)
    {
      Details = details;
      Message = message;
      StatusCode = statusCode;

    }
  }
}