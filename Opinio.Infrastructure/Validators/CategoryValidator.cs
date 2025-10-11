using FluentValidation;
using Opinio.Core.Entities;

namespace Opinio.Infrastructure.Validators;

public class CategoryValidator : AbstractValidator<Category>
{

    public CategoryValidator()
    {

        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Category Name Is Required")
           .MaximumLength(100).WithMessage("Category name Exceed Max Length");

        RuleFor(x => x.ImageUrl)
           .MaximumLength(500).WithMessage("InValid Image Url");
    }
}
