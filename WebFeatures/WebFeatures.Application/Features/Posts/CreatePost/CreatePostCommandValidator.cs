using FluentValidation;
using WebFeatures.Application.Extensions;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator(IAppContext context)
        {
            RuleFor(x => x.BlogId)
                .Must(b => context.Set<Blog>().Exists(b))
                .WithMessage(b => ValidationErrorMessages.NotExistsInDb(typeof(Blog)));
        }
    }
}
