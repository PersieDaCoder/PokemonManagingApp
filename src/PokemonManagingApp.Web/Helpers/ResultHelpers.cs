using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace PokemonManagingApp.Web.Helpers;

public static class ResultHelpers
{
    public static bool IsNotFound<T>(this Result<T> result)
    {
        return result.Errors.Any(error => error.Contains("not found", StringComparison.OrdinalIgnoreCase));
    }
}