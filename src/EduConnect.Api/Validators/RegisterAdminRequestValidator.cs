using EduConnect.Api.Dtos;
using FluentValidation;

namespace EduConnect.Api.Validators;

public class RegisterAdminRequestValidator : AbstractValidator<RegisterAdminRequest>
{
    public RegisterAdminRequestValidator()
    {
        RuleFor(x => x.TokenOfAcademy)
            .NotEmpty().WithMessage("Academy Token is required")
            .MinimumLength(6).WithMessage("Academy Token must be 6 characters long");
    }
}