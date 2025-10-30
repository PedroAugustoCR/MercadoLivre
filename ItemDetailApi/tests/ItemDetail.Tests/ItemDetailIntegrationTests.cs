using System.Text.Json;
using FluentAssertions;
using ItemDetail.Application.Common.Exceptions;
using ItemDetail.Application.UseCases.GetProductById;
using ItemDetail.Domain.Abstractions;
using ItemDetail.Domain.Entities;

public class ItemDetailIntegrationTests
{
    [Fact]
    public async Task Handler_returns_dto_for_existing_id_using_real_dataset()
    {
        var products = await LoadProductsAsync();
        products.Should().NotBeEmpty("precisamos de pelo menos 1 item no products.json");

        var repo = new InMemoryProductRepository(products);
        var handler = new GetProductByIdHandler(repo);

        var firstId = products[0].Id;

        // Act
        var dto = await handler.Handle(new GetProductByIdQuery(firstId));

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(firstId);
        dto.Title.Should().NotBeNullOrWhiteSpace();
        dto.CurrencyId.Should().Be("BRL");
        dto.Price.Should().BeGreaterOrEqualTo(0);
        dto.Pictures.Should().NotBeNull();
        dto.Attributes.Should().NotBeNull();
    }

    [Fact]
    public async Task Handler_throws_NotFound_for_unknown_id()
    {
        var repo = new InMemoryProductRepository(await LoadProductsAsync());
        var handler = new GetProductByIdHandler(repo);

        await Assert.ThrowsAsync<NotFoundException>(
            () => handler.Handle(new GetProductByIdQuery("__id_inexistente__")));
    }

    private async Task<List<Product>> LoadProductsAsync()
{
    var path = Path.Combine(AppContext.BaseDirectory, "Data", "products.json");
    await using var fs = File.OpenRead(path);
    var list = await JsonSerializer.DeserializeAsync<List<Product>>(fs);
    return list ?? new List<Product>();
}

    private sealed class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<string, Product> _map;

        public InMemoryProductRepository(IEnumerable<Product> products)
        {
            _map = products.GroupBy(p => p.Id, StringComparer.OrdinalIgnoreCase)
                           .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
        }

        public Task<Product?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            _map.TryGetValue(id, out var p);
            return Task.FromResult<Product?>(p);
        }
    }
}
