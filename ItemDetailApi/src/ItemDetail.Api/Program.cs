using System.Reflection;
using ItemDetail.Api.Endpoints;
using ItemDetail.Api.Middleware;
using ItemDetail.Application.UseCases.GetProductById;
using ItemDetail.Domain.Abstractions;
using ItemDetail.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductRepository>(_ => new JsonProductRepository());
builder.Services.AddTransient<GetProductByIdHandler>();

var app = builder.Build();

app.UseGlobalExceptionHandling();

app.MapGet("/openapi/v1/openapi.yaml", async context =>
{
    var path = Path.Combine(app.Environment.ContentRootPath, "openapi", "openapi.yaml");
    context.Response.ContentType = "application/yaml";
    await context.Response.SendFileAsync(path);
});

app.UseSwaggerUI(o =>
{
    o.RoutePrefix = "docs";
    o.SwaggerEndpoint("/openapi/v1/openapi.yaml", "Item Detail API (OpenAPI 3.0)");
});

app.MapHealthEndpoints();
app.MapItemEndpoints();

app.Run();
