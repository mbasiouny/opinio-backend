using FluentValidation;
using Opinio.Core.Entities;

namespace Opinio.Infrastructure.Validators;

public class EntityValidator : AbstractValidator<Entity>
{

    public EntityValidator()
    {

        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Entity Name Is Required")
           .MaximumLength(255).WithMessage("Entity name Exceed Max Length");

        RuleFor(x => x.CategoryId)
           .NotEmpty().WithMessage("Category Id Is Required")
           .GreaterThan(0).WithMessage("Invalid Category Id");

        RuleFor(x => x.Website)
           .MaximumLength(255).WithMessage("Website Url Exceed Max Length");
    }
}
