using FluentValidation;
using WebFeatures.Application.Infrastructure.Failures;
using WebFeatures.Application.Pipeline.Abstractions;
using ValidationException = WebFeatures.Application.Infrastructure.Exceptions.ValidationException;

namespace WebFeatures.Application.Pipeline.Implementations
{
    /// <summary>
    /// Валидация входных данных запроса
    /// </summary>
    public class ValidationHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly IValidator<TIn> _validator;

        public ValidationHandlerDecorator(IHandler<TIn, TOut> decoratee, IValidator<TIn> validator) : base(decoratee)
        {
            _validator = validator;
        }

        public override TOut Handle(TIn input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                var fail = new Fail(validationResult.Errors);
                throw new ValidationException(fail);
            }

            return Decoratee.Handle(input);
        }
    }
}
