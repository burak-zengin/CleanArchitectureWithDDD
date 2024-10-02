using Application.Products.Create;
using Domain.Products;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;

namespace Application.UnitTests.Products;

public class CreateTests
{
    private static readonly Command _command = new(
        "24Y010001",
        [
            new Application.Products.Create.ProductItem(
                "1234567891234",
                "Black",
                "XL",
                "TL",
                100
            )
        ]
    );

    private readonly IProductRepository _repository;
    private readonly Handler _handler;

    public CreateTests()
    {
        _repository = NSubstitute.Substitute.For<IProductRepository>();
        _handler = new Handler(_repository);
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenModelCodeIsInvalid()
    {
        //Arrange
        var command = _command with { ModelCode = "Invalid Model Code" };

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
            ProductItems = [new(
            "Invalid Barcode",
            "Black",
            "XL",
            "TL",
            100)]
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
            ProductItems = [new(
            "12345",
            "Black",
            "XL",
            "TL",
            0)]
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
            ProductItems = [new(
            "12345",
            "Black",
            "XL",
            "Invalid Currency",
            1000)]
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenDuplicateBarcode()
    {
        //Arrange
        var command = _command with
        {
            ProductItems = [
                new(
                    "1234567891234",
                    "Black",
                    "L",
                    "TL",
                    1000
                ),
                new(
                    "1234567891234",
                    "Black",
                    "XL",
                    "TL",
                    1000
                )
            ]
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenDuplicateSizeAndColor()
    {
        //Arrange
        var command = _command with
        {
            ProductItems = [
                new(
                    "1234567891234",
                    "Black",
                    "L",
                    "TL",
                    1000
                ),
                new(
                    "1234567891234",
                    "Black",
                    "XL",
                    "TL",
                    1000
                )
            ]
        };

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnErrorWhenModelCodeIsNotUnique()
    {
        //Arrange
        _repository.AnyAsync(Arg.Is<ModelCode>(_ => _.Value == _command.ModelCode), default).Returns(true);

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.Failed.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShouldReturnDataWhenDoesNotFail()
    {
        //Arrange

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        using (new AssertionScope())
        {
            result.Failed.Should().BeFalse();
            result.Data.Should().NotBeEmpty();
        }
    }
}
