using FluentValidation;
using Web.Infrastructure;

namespace Web.Features.Authentication
{
    /// <summary>
    /// Валидация входа в систему
    /// </summary>
    public class LoginValidator : AbstractValidator<Login>
    {
        /// <inheritdoc />
        public LoginValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);
        }
    }
}
