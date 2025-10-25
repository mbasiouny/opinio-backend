using FluentValidation;
using Opinio.Core.Entities;

namespace Opinio.Infrastructure.Validators;

public class ReviewImageValidator : AbstractValidator<ReviewImage>
{
    public ReviewImageValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL is required.")
            .MaximumLength(500).WithMessage("Image URL cannot exceed 500 characters.");
    }
}
