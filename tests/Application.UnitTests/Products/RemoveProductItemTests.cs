using Application.Products.RemoveProduct;
using Domain.Products;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Products;

public class RemoveProductItemTests
{
    private readonly static Command _command = new(
        Guid.NewGuid(),
        Guid.NewGuid());

    private readonly IProductRepository _repository;
    private readonly Handler _handler;

    public RemoveProductItemTests()
    {
        _repository = NSubstitute.Substitute.For<IProductRepository>();
        _handler = new Handler(_repository);
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenProductIdIsInvalid()
    {
        //Arrange
        var command = _command with
        {
            ProductId = Guid.Empty
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenIdIsInvalid()
    {
        //Arrange
        var command = _command with
        {
            Id = Guid.Empty
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenProductNotFound()
    {
        //Arrange
        _repository.GetAsync(Arg.Is<ProductId>(_ => _.Value == _command.ProductId), default).ReturnsNull();

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenProductItemNotFound()
    {
        //Arrange
        var product = Product.Create(Guid.NewGuid(), "24Y010001").Data;
        product.AddProduct(ProductItem.Create(Guid.NewGuid(), product.Id, "1234567891234", "Black", "XL", "TL", 1000).Data);

        _repository.GetAsync(Arg.Is<ProductId>(_ => _.Value == _command.ProductId), default).Returns(product);

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnSuccessWhenDoesNotFail()
    {
        //Arrange
        var id = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var command = _command with { Id = id, ProductId = productId };

        var product = Product.Create(productId, "24Y010001").Data;
        product.AddProduct(ProductItem.Create(id, product.Id, "1234567891234", "Black", "XL", "TL", 1000).Data);

        _repository.GetAsync(Arg.Is<ProductId>(_ => _.Value == command.ProductId), default).Returns(product);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeFalse();
    }
}
