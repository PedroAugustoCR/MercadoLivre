namespace ItemDetail.Domain.Entities;

public sealed class Product
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string CurrencyId { get; init; }
    public decimal Price { get; init; }
    public string Condition { get; init; } = "new";
    public int AvailableQuantity { get; init; }
    public int SoldQuantity { get; init; }
    public IReadOnlyList<string> Pictures { get; init; } = Array.Empty<string>();
    public IReadOnlyDictionary<string, string> Attributes { get; init; } = new Dictionary<string, string>();
    public bool FreeShipping { get; init; }
    public string Warranty { get; init; } = "";
}
