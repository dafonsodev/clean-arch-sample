using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact(DisplayName = "Create Product With Valid State")]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product Negative Id Value")]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid id value.");
    }

    [Fact(DisplayName = "Create Product Short Name Value")]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product Image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Minimum 3 characters.");
    }

    [Fact(DisplayName = "Create Product Long Image Name")]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99,
            "Product Image toooooooooooooooooooo loooooooooooooooooonnnnnnnnnnnnnnnnnnnggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image. Maximum 250 characters.");
    }

    [Fact(DisplayName = "Create Product With Null Image Name Domain")]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product With Null Image Name Reference")]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should()
            .NotThrow<NullReferenceException>();
    }

    [Fact(DisplayName = "Create Product With Empty Image Name")]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product Invalid Price Value")]
    public void CreateProduct_InvalidPriceValue_DomainExceptionInvalidPrice()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "Product Image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value.");
    }

    [Theory(DisplayName = "Create Product Invalid Stock Value")]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_DomainExceptionNegativeValue(int value)
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product Image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value.");
    }
}