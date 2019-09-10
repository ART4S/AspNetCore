using FluentValidation;
using System.Linq;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
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
