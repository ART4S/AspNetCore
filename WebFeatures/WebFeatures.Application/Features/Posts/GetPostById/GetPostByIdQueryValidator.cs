﻿using FluentValidation;
using WebFeatures.Application.Extensions;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator(IAppContext context)
        {
            RuleFor(x => x.Id)
                .Must(x => context.Set<Post>().Exists(x))
                .WithMessage(ValidationErrorMessages.NotExistsInDb(typeof(Post)));
        }
    }
}