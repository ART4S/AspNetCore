using FluentValidation;
using WebFeatures.Application.Extensions;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator(IAppContext context)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.AuthorId)
                .Must(x => context.Set<User>().Exists(x))
                .WithMessage(ValidationErrorMessages.NotExistsInDb(typeof(User)));
        }
    }
}
