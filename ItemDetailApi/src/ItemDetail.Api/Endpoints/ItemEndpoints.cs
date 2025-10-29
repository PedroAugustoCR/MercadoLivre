using ItemDetail.Application.UseCases.GetProductById;
namespace ItemDetail.Api.Endpoints;

using Swashbuckle.AspNetCore.Annotations;
using ItemDetail.Application.DTOs;

public static class ItemEndpoints
{
    public static RouteGroupBuilder MapItemEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/api/items")
            .WithTags("Items");

        // GET /api/items/{id}
        group.WithTags("Items");

        group.MapGet("/{id}",
        [SwaggerOperation(
            Summary = "Obtém detalhes de um item (Produto)",
            Description = "Retorna informações completas do item (título, preço, atributos, fotos, etc.).",
            OperationId = "Item_GetById"
        )]
        async (string id, GetProductByIdHandler handler, CancellationToken ct) =>
        {
            var dto = await handler.Handle(new GetProductByIdQuery(id), ct);
            return Results.Ok(dto);
        })
        .Produces<ProductDto>(StatusCodes.Status200OK)
        .Produces<Microsoft.AspNetCore.Mvc.ProblemDetails>(StatusCodes.Status404NotFound)
        .WithOpenApi();

        return group;
    }
}
