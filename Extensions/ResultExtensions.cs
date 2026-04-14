using Microsoft.AspNetCore.Mvc;
using Platform.SharedKernel.Responses;

namespace Platform.SharedKernel.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        var response = new
        {
            Success = result.IsSuccess,
            Data = result.Value,
            Errors = result.Errors
        };

        if (result.IsSuccess)
            return new OkObjectResult(response);

        if (result.Errors != null && result.Errors.Any(e => e.Contains("Unauthorized")))
            return new UnauthorizedObjectResult(response);

        return new BadRequestObjectResult(response);
    }
}
