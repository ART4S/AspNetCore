using FluentValidation;
using Web.Infrastructure.Exceptions;
using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Results;

namespace Web.Infrastructure.Pipeline.Implementations
{
    class ValidationHandlerDecorator<TRequest, TResponse> : HandlerDecoratorBase<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationHandlerDecorator(IRequestHandler<TRequest, TResponse> decoratee, IValidator<TRequest> validator) : base(decoratee)
        {
            _validator = validator;
        }

        public override TResponse Handle(TRequest request)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                var failureResult = Result.Fail(result.Errors);
                throw new ResultException(failureResult);
            }

            return Decoratee.Handle(request);
        }
    }
}
