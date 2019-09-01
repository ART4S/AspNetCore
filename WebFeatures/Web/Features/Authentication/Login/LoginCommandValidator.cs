using FluentValidation;
using Web.Infrastructure.Validation;

namespace Web.Features.Authentication.Login
{
    /// <summary>
    /// Валидация входа в систему
    /// </summary>
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        /// <inheritdoc />
        public LoginCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);
        }
    }
}
