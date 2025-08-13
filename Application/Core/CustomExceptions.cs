namespace DNAPayments.AccountManagement.Application;

public class AppException(int statusCode, string message, string details = null)
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
}


public class InvalidRequestException(string message, string? data) : Exception(data ?? "Incorrect request")
{
    public string Message { get; set; } = message;
}

public class UserNotAuthorizedException(string? message) : Exception(message ?? "User not authorized");