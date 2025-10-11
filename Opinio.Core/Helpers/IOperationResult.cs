namespace Opinio.Core.Helpers;

public interface IOperationResult<T>
{
    T Data { get; set; }

    Dictionary<string, List<string>> ValidationErrors { get; set; }

    string Message { get; set; }

    OperationStatus Status { get; set; }
}
