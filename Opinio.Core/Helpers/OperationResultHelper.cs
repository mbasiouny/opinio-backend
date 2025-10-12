namespace Opinio.Core.Helpers;

public static class OperationResultHelper
{
    public static OperationResult<T> CreateValidationError<T>(string fieldName, string errorMessage)
    {
        return OperationResult<T>.ValidationError(
            new Dictionary<string, List<string>>
            {
                    { fieldName, new List<string> { errorMessage } }
            }
        );
    }
}
