using FluentValidation;
using Web.Infrastructure.Exceptions;
using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Infrastructure.Pipeline.Implementations
{
    class ValidationHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly IValidator<TIn> _validator;

        public ValidationHandlerDecorator(IHandler<TIn, TOut> decoratee, IValidator<TIn> validator) : base(decoratee)
        {
            _validator = validator;
        }

        public override TOut Handle(TIn input)
        {
            var result = _validator.Validate(input);
            if (!result.IsValid)
            {
                var failureResult = Result.Fail(result.Errors);
                throw new ResultException(failureResult);
            }

            return Decoratee.Handle(input);
        }
    }
}
