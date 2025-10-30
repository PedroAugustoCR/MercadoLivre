using FluentAssertions;
using Moq;
using ItemDetail.Domain.Abstractions;
using ItemDetail.Domain.Entities;
using ItemDetail.Application.UseCases.GetProductById;
using ItemDetail.Application.Common.Exceptions;

namespace ItemDetail.Tests;

public class ItemDetailUnitTests
{
    [Theory]
    [InlineData("MLB-123")]
    [InlineData("abc-XYZ-999")]
    public async Task Returns_product_dto_when_found_basic_mapping(string id)
    {
        // Arrange
        var product = new Product
        {
            Id = id,
            Title = "Test",
            CurrencyId = "BRL",
            Price = 10M,
            Condition = "new",
            AvailableQuantity = 1,
            SoldQuantity = 0,
            Pictures = new[] { "http://x/img1.jpg", "http://x/img2.jpg" },
            Attributes = new Dictionary<string, string> { ["Marca"] = "Futura" },
            FreeShipping = true,
            Warranty = ""
        };

        var repo = new Mock<IProductRepository>(MockBehavior.Strict);
        repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var handler = new GetProductByIdHandler(repo.Object);

        // Act
        var result = await handler.Handle(new GetProductByIdQuery(id));

        // Assert
        result.Id.Should().Be(id);
        result.Title.Should().Be("Test");
        result.CurrencyId.Should().Be("BRL");
        result.Price.Should().Be(10M);
        result.Condition.Should().Be("new");
        result.AvailableQuantity.Should().Be(1);
        result.SoldQuantity.Should().Be(0);
        result.FreeShipping.Should().BeTrue();
        result.Pictures.Should().HaveCount(2);
        result.Attributes.Should().ContainKey("Marca").WhoseValue.Should().Be("Futura");

        repo.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        repo.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData("missing")]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Throws_NotFound_when_missing_or_empty_id(string id)
    {
        // Arrange
        var repo = new Mock<IProductRepository>(MockBehavior.Strict);
        repo.Setup(r => r.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var handler = new GetProductByIdHandler(repo.Object);

        // Act / Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetProductByIdQuery(id)));

        repo.Verify(r => r.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        repo.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Maps_defaults_when_repo_returns_missing_fields_in_json()
    {
        var product = new Product
        {
            Id = "MLB-DEFAULTS",
            Title = "Sem campos opcionais",
            CurrencyId = "BRL",
            Price = 0M,
            Condition = "new",
            AvailableQuantity = 0,      // defaults
            SoldQuantity = 0,
            Pictures = Array.Empty<string>(),
            Attributes = new Dictionary<string, string>(),
            FreeShipping = false,
            Warranty = ""
        };

        var repo = new Mock<IProductRepository>();
        repo.Setup(r => r.GetByIdAsync("MLB-DEFAULTS", It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var handler = new GetProductByIdHandler(repo.Object);

        var dto = await handler.Handle(new GetProductByIdQuery("MLB-DEFAULTS"));

        dto.AvailableQuantity.Should().Be(0);
        dto.SoldQuantity.Should().Be(0);
        dto.FreeShipping.Should().BeFalse();
        dto.Pictures.Should().BeEmpty();
        dto.Attributes.Should().BeEmpty();
    }
}
