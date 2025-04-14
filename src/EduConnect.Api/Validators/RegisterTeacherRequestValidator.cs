using EduConnect.Api.Dtos;
using FluentValidation;

namespace EduConnect.Api.Validators;

public class RegisterTeacherRequestValidator : AbstractValidator<RegisterTeacherRequest>
{
    public RegisterTeacherRequestValidator()
    {
        RuleFor(x => x.TokenForTeacher)
            .NotEmpty().WithMessage("Teacher Token is required")
            .MinimumLength(6).WithMessage("Teacher Token must be 6 characters long");
    }
}