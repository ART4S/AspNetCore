using FluentValidation;
using WebFeatures.Application.Extensions;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator(IAppContext context)
        {
            RuleFor(x => x.Id)
                .Must(x => context.Set<Post>().Exists(x))
                .WithMessage(ValidationErrorMessages.NotExistsInDb(typeof(Post)));

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);
        }
    }
}
