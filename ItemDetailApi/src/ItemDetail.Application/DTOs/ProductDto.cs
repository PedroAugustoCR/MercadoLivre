namespace ItemDetail.Application.DTOs;

public sealed record ProductDto(
    string Id,
    string Title,
    string CurrencyId,
    decimal Price,
    string Condition,
    int AvailableQuantity,
    int SoldQuantity,
    IReadOnlyList<string> Pictures,
    IReadOnlyDictionary<string, string> Attributes,
    bool FreeShipping,
    string Warranty
);
