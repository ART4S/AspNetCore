using DataContext;
using FluentValidation;
using System.Linq;
using Web.Infrastructure.Validation;
using WebFeatures.DataContext;
using WebFeatures.Entities.Model;

namespace Web.Features.Blogs.CreateBlog
{
    /// <summary>
    /// Валидация команды на создание блога
    /// </summary>
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public CreateBlogCommandValidator(IAppContext context)
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.AuthorId)
                .Must(x => context.Set<User>().Any(y => y.Id == x)).WithMessage(x => $"Пользователь c id='{x}' не найден");
        }
    }
}
