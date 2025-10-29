using ItemDetail.Application.Common.Exceptions;
using ItemDetail.Application.DTOs;
using ItemDetail.Domain.Abstractions;

namespace ItemDetail.Application.UseCases.GetProductById;

public sealed class GetProductByIdHandler
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
        => _repository = repository;

    public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken ct = default)
    {
        var product = await _repository.GetByIdAsync(query.Id, ct);
        if (product is null)
            throw new NotFoundException($"Produto '{query.Id}' não encontrado.");

        return new ProductDto(
            product.Id,
            product.Title,
            product.CurrencyId,
            product.Price,
            product.Condition,
            product.AvailableQuantity,
            product.SoldQuantity,
            product.Pictures,
            product.Attributes,
            product.FreeShipping,
            product.Warranty
        );
    }
}
