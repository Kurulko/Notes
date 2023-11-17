using Microsoft.AspNetCore.Mvc;
using Notes.Interfaces.Maps.AuthMaps;
using WebApi.Controllers.AuthControllers;

namespace WebApi.Controllers;

[Route($"{pathApi}/[controller]")]
public class ApiController : ControllerBase
{
    protected const string pathApi = "api";
    protected readonly ILogger<ApiController> logger;
    public ApiController(ILogger<ApiController> logger)
        => this.logger = logger;

    protected async Task<IActionResult> ReturnOkIfEverithingIsGood(Func<Task> action)
    {
        try
        {
            await action();
            return Ok();
        }
        catch (Exception ex)
        {
            return ReturnProblemDetails(ex);
        }
    }

    protected async Task<IActionResult> ReturnOkIfEverithingIsGood<T>(Func<Task<T>> action)
    {
        try
        {
            return Ok(await action());
        }
        catch (Exception ex)
        {
            return ReturnProblemDetails(ex);
        }
    }

    protected IActionResult ReturnProblemDetails(Exception ex)
    {
        Exception? innerEx = ex.InnerException;
        string errorsMessage = (innerEx is null ? ex : innerEx).Message;

        var errors = new Dictionary<string, string[]>
            {
                { "Exception", new string[] { errorsMessage } },
            };

        ProblemDetails problemDetails = new()
        {
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Extensions = { ["errors"] = errors }
        };

        return BadRequest(problemDetails);
    }
}