using System.Net.Mime;
using ItemDetail.Application.Common.Exceptions;
using ItemDetail.Application.UseCases.GetProductById;
using ItemDetail.Domain.Abstractions;
using ItemDetail.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddSingleton<IProductRepository>(_ => new JsonProductRepository());
builder.Services.AddTransient<GetProductByIdHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // o JSON continua no caminho padrão:
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Item Detail API v1");
    // só mudamos a rota da UI:
    options.RoutePrefix = "docs"; // abre em /docs
});


// middleware de erro simples
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (NotFoundException nf)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsJsonAsync(new { error = nf.Message });
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { error = "Unexpected server error", detail = ex.Message });
    }
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapGet("/api/items/{id}", async (string id, GetProductByIdHandler handler, CancellationToken ct) =>
{
    var dto = await handler.Handle(new GetProductByIdQuery(id), ct);
    return Results.Ok(dto);
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.Run();
