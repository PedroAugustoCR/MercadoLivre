using System.Text.Json;
using ItemDetail.Domain.Abstractions;
using ItemDetail.Domain.Entities;

namespace ItemDetail.Infrastructure.Repositories;

public sealed class JsonProductRepository : IProductRepository
{
    private readonly string _dataFilePath;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);
    private IReadOnlyDictionary<string, Product>? _cache;

    public JsonProductRepository(string? dataFilePath = null)
    {
        _dataFilePath = dataFilePath ?? Path.Combine(AppContext.BaseDirectory, "Data", "products.json");
    }

    public async Task<Product?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var all = await LoadAllAsync(ct);
        return all.TryGetValue(id, out var p) ? p : null;
    }

    private async Task<IReadOnlyDictionary<string, Product>> LoadAllAsync(CancellationToken ct)
    {
        if (_cache is not null) return _cache;

        if (!File.Exists(_dataFilePath))
            throw new FileNotFoundException($"Data file not found: {_dataFilePath}");

        await using var fs = File.OpenRead(_dataFilePath);
        var items = await JsonSerializer.DeserializeAsync<List<Product>>(fs, _jsonOptions, ct)
                    ?? new List<Product>();

        _cache = items.ToDictionary(p => p.Id, p => p, StringComparer.OrdinalIgnoreCase);
        return _cache;
    }
}
