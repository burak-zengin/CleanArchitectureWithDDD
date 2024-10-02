using Application.Products.AddProductItem;
using Domain.Products;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Products;

public class AddProductItemTests
{
    private static readonly Command _command = new(
        Guid.NewGuid(),
        "1234567891234",
        "Black",
        "XL",
        "TL",
        1000);

    private readonly IProductRepository _repository;
    private readonly Handler _handler;

    public AddProductItemTests()
    {
        _repository = NSubstitute.Substitute.For<IProductRepository>();
        _handler = new Handler(_repository);
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenProductIsInvalid()
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
    public async Task HandleShouldReturnErrorWhenBarcodeIsInvalid()
    {
        //Arrange
        var command = _command with
        {
            Barcode = "Invalid Barcode"
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenAmountIsInvalid()
    {
        //Arrange
        var command = _command with
        {
            Amount = 0
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenCurrencyIsInvalid()
    {
        //Arrange
        var command = _command with
        {
            Currency = "Invalid Currency"
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenBarcodeIsNotUnique()
    {
        //Arrange
        _repository.AnyAsync(Arg.Is<Barcode>(_ => _.Value == _command.Barcode), default).Returns(true);

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnSuccessWhenDoesNotFail()
    {
        //Arrange
        var product = Product.Create(Guid.NewGuid(), "24Y010001").Data;
        _repository.GetAsync(Arg.Is<ProductId>(_ => _.Value == _command.ProductId), default).Returns(product);

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.Failed.Should().BeFalse();
    }
}
