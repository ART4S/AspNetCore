using Entities.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Web.Features.Registration
{
    /// <summary>
    /// Валидация регистрации пользователя
    /// </summary>
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        /// <inheritdoc />
        public RegisterUserValidator(DbContext dbContext)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MinimumLength(3).WithMessage("Имя должно состоять минимум из 3-х символов")
                .MaximumLength(15).WithMessage("Имя должно состоять максимум из 15-и символов")
                .Must(n => dbContext.Set<User>().All(y => n != y.Name))
                    .WithMessage("Пользователь с данным именем уже зарегестрирован");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .EmailAddress().WithMessage("Некорректный e-mail")
                .Must(e => dbContext.Set<User>().All(x => x.Email != e))
                    .WithMessage("Пользователь с данным e-mail уже зарегистрирован");


            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Пароль должен состоять минимум из 8 символов")
                .Matches(@"[A-Z]").WithMessage("Пароль должен содержать минимум 1 букву в верхнем регистре")
                .Matches(@"[a-z]").WithMessage("Пароль должен содержать минимум 1 букву в нижнем регистре")
                .Matches(@"\d").WithMessage("Пароль должен содержать минимум 1 цифру")
                .Matches(@"[#?!@$%^&*-]").WithMessage("Пароль должен содержать минимум 1 из специальных '#?!@$%^&*-'");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Подтверждение пароля не совпадает");
        }
    }
}
