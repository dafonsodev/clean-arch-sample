using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category With Valid State")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () =>
        {
            Category category = new(1, "Category Name");
        };
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Category Negative Id Value")]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () =>
        {
            Category category = new(-1, "Category Name");
        };
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid id value.");
    }

    [Fact(DisplayName = "Create Category Short Name Value")]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () =>
        {
            Category category = new(1, "Ca");
        };
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Minimum 3 characters.");
    }

    [Fact(DisplayName = "Create Category Missing Name Value")]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () =>
        {
            Category category = new(1, "");
        };
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required.");
    }

    [Fact(DisplayName = "Create Category With Null Name Value")]
    public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () =>
        {
            Category category = new(1, null);
        };
        action.Should()
            .Throw<Validation.DomainExceptionValidation>();
    }
}
