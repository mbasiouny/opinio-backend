using FluentValidation.Results;

namespace Opinio.Infrastructure.Extensions;

public static class ValidationExtensions
{
    public static Dictionary<string, List<string>> ToErrorDictionary(this ValidationResult validationResult)
    {
        return (from e in validationResult.Errors
                group e by e.PropertyName).ToDictionary((IGrouping<string, ValidationFailure> g) => g.Key, (IGrouping<string, ValidationFailure> g) => g.Select((ValidationFailure e) => e.ErrorMessage).ToList());
    }
}
