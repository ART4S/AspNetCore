using Microsoft.Extensions.Logging;
using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Pipeline.Implementations
{
    /// <summary>
    /// Декоратор логирования
    /// </summary>
    class LoggingHandlerDecorator<TRequest, TResult> : HandlerDecoratorBase<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        private readonly ILogger<TRequest> _logger;

        /// <inheritdoc />
        public LoggingHandlerDecorator(IRequestHandler<TRequest, TResult> decorated, ILogger<TRequest> logger) : base(decorated)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public override TResult Handle(TRequest request)
        {
            var result = Decoratee.Handle(request);
            _logger.LogInformation($"{Decoratee.GetType().Name} : {request.ToString()} => {result.ToString()}");
            return result;
        }
    }
}
