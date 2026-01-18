using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common
{
    public static class StatusHandler
    {
        public static IActionResult HandleResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Value);

            return controller.BadRequest(result.ErrorItems);
        }
    }
}