using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validator
{
   
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(temp => temp.Email)
                .NotEmpty().WithMessage("Please Enter EmailID.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(temp => temp.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
