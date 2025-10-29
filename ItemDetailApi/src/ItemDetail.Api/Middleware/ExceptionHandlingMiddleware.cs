using System.Net.Mime;
using ItemDetail.Application.Common.Exceptions;

namespace ItemDetail.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException nf)
        {
            _logger.LogWarning(nf, "Recurso não encontrado: {Message}", nf.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsJsonAsync(new { error = nf.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unexpected server error",
                detail = ex.Message
            });
        }
    }
}
