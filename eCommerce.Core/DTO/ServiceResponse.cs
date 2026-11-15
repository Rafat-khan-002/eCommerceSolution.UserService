namespace eCommerce.Core.DTO;


public class ServiceResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public ErrorResponse? Error { get; set; }
    public string? Message { get; set; }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public ErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
