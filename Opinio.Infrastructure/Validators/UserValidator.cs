using FluentValidation;
using Opinio.Core.Entities;

namespace Opinio.Infrastructure.Validators;

public class UserValidator : AbstractValidator<User>
{

    public UserValidator()
    {

        RuleFor(x => x.Username)
           .NotEmpty().WithMessage("User Name Is Required")
           .MaximumLength(100).WithMessage("User name Exceed Max Length");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}
