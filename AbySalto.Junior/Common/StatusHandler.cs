using Microsoft.AspNetCore.Mvc;
using Application.Common;

namespace AbySalto.Junior.Common
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
