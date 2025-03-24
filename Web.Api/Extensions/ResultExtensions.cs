using HogwartsAPI.SharedKernel;
using Microsoft.AspNetCore.Mvc;
namespace HogwartsAPI.Web.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToResponse(this Result result)
        {
            if (result.IsSuccess)
            {
                return new ObjectResult(result.Success)
                {
                    StatusCode = result.Success.StatusCode,
                };
            }
            return new ObjectResult(result.Error)
            {
                StatusCode = result.Error.StatusCode,
            };
        }
    }
}