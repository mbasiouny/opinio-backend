using FluentValidation;
using Opinio.Core.Entities;

namespace Opinio.Infrastructure.Validators;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(x => x.EntityId)
            .GreaterThan(0)
            .WithMessage("EntityId is required and must be greater than 0.");

        RuleFor(x => x.DateOfTrial)
            .NotEmpty()
            .WithMessage("Date of trial is required.")
            .Must(date => date <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of trial cannot be in the future.");

        RuleFor(x => x.ReviewText)
            .NotEmpty().WithMessage("Review text is required.")
            .MaximumLength(2000).WithMessage("Review text cannot exceed 2000 characters.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.ItemsBought)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrEmpty(x.ItemsBought))
            .WithMessage("ItemsBought cannot exceed 1000 characters.");

        RuleFor(x => x.ItemsBought)
            .NotEmpty()
            .When(x => x.IsConsumer == true)
            .WithMessage("ItemsBought is required when user is a consumer.");

        RuleForEach(x => x.Images)
            .SetValidator(new ReviewImageValidator())
            .When(x => x.Images != null && x.Images.Any());
    }
}
