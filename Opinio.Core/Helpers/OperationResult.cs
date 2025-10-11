namespace Opinio.Core.Helpers;

public class OperationResult<T> : IOperationResult<T>
{
    public T Data { get; set; }

    public Dictionary<string, List<string>> ValidationErrors { get; set; }

    public string Message { get; set; }

    public int? HttpCode { get; set; }
    public OperationStatus Status { get; set; }

    public bool IsSuccess { get; set; }

    public OperationResult(T data, Dictionary<string, List<string>> validationErrors, string message, int? httpCode, OperationStatus status, bool isSuccess = false)
    {
        Data = data;
        ValidationErrors = validationErrors;
        Message = message;
        HttpCode = httpCode;
        Status = status;
        IsSuccess = isSuccess;
    }

    public static OperationResult<T> Success(T data, string message = "Request was successful.")
    {
        return new OperationResult<T>(data, null, message, 200, OperationStatus.Success, isSuccess: true);
    }

    public static OperationResult<T> Failure(Dictionary<string, List<string>> validationErrors = null, string message = "Request failed.")
    {
        return new OperationResult<T>(default(T), validationErrors, message, 500, OperationStatus.Failure);
    }

    public static OperationResult<T> NotFound(string message = "Resource not found.")
    {
        return new OperationResult<T>(default(T), null, message, 404, OperationStatus.NotFound);
    }

    public static OperationResult<T> Unauthorized(string message = "Unauthorized access.")
    {
        return new OperationResult<T>(default(T), null, message, 401, OperationStatus.Unauthorized);
    }

    public static OperationResult<T> Forbidden(string message = "Forbidden access.")
    {
        return new OperationResult<T>(default(T), null, message, 403, OperationStatus.Forbidden);
    }

    public static OperationResult<T> ValidationError(Dictionary<string, List<string>> validationErrors)
    {
        return new OperationResult<T>(default(T), validationErrors, "Validation errors occurred.", 400, OperationStatus.ValidationError);
    }

    public static OperationResult<T> ValidationError(Dictionary<string, List<string>> validationErrors = null, string message = null)
    {
        if (message == null)
        {
            message = "Resource not found.";
        }

        return new OperationResult<T>(default(T), validationErrors, message, 400, OperationStatus.ValidationError);
    }

    public static OperationResult<T> BusinessError(Dictionary<string, List<string>> validationErrors = null, string message = null)
    {
        if (message == null)
        {
            message = "Logical errors occurred.";
        }

        return new OperationResult<T>(default(T), validationErrors, message, 200, OperationStatus.BusinessError);
    }
}
