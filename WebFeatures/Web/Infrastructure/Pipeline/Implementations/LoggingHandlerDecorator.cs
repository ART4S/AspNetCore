using Microsoft.Extensions.Logging;
using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Pipeline.Implementations
{
    /// <summary>
    /// Декоратор логирования
    /// </summary>
    class LoggingHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly ILogger<TIn> _logger;

        /// <inheritdoc />
        public LoggingHandlerDecorator(IHandler<TIn, TOut> decorated, ILogger<TIn> logger) : base(decorated)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public override TOut Handle(TIn input)
        {
            var result = Decoratee.Handle(input);
            _logger.LogInformation($"{Decoratee.GetType().Name} : {input.ToString()} => {result.ToString()}");
            return result;
        }
    }
}
