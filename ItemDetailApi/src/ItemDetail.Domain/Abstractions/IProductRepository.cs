using ItemDetail.Domain.Entities;

namespace ItemDetail.Domain.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(string id, CancellationToken ct = default);
}
