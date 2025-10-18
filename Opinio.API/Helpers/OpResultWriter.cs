using System.Text.Json;
using Opinio.Core.Helpers;

namespace Opinio.API.Helper;

public static class OpResultWriter
{
    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        PropertyNamingPolicy = null,
        WriteIndented = false
    };

    public static async Task WriteAsync<T>(HttpResponse resp, OperationResult<T> op)
    {
        resp.ContentType = "application/json; charset=utf-8";
        resp.StatusCode = op.HttpCode ?? StatusCodes.Status200OK;
        var json = JsonSerializer.Serialize(op, _jsonOpts);
        await resp.WriteAsync(json);
    }
}
