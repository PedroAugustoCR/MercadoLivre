using FluentAssertions;
using Moq;
using ItemDetail.Domain.Abstractions;
using ItemDetail.Domain.Entities;
using ItemDetail.Application.UseCases.GetProductById;
using ItemDetail.Application.Common.Exceptions;

namespace ItemDetail.Tests;

public class GetProductByIdTests
{
    [Fact]
    public async Task Returns_product_dto_when_found()
    {
        var repo = new Mock<IProductRepository>();
        repo.Setup(r => r.GetByIdAsync("MLB-123", default)).ReturnsAsync(new Product {
            Id = "MLB-123",
            Title = "Test",
            CurrencyId = "BRL",
            Price = 10M,
            Condition = "new",
            AvailableQuantity = 1,
            SoldQuantity = 0,
            Pictures = new [] { "http://x" },
            Attributes = new Dictionary<string,string>(),
            FreeShipping = true,
            Warranty = ""
        });

        var handler = new GetProductByIdHandler(repo.Object);
        var result = await handler.Handle(new GetProductByIdQuery("MLB-123"));
        result.Id.Should().Be("MLB-123");
        result.Title.Should().Be("Test");
    }

    [Fact]
    public async Task Throws_NotFound_when_missing()
    {
        var repo = new Mock<IProductRepository>();
        repo.Setup(r => r.GetByIdAsync("missing", default)).ReturnsAsync((Product?)null);
        var handler = new GetProductByIdHandler(repo.Object);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetProductByIdQuery("missing")));
    }
}
