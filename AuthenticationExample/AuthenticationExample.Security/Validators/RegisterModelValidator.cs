using AuthenticationExample.Security.Models;
using FluentValidation;

namespace AuthenticationExample.Security.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}
