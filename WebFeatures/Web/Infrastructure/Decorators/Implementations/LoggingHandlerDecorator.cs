using Microsoft.Extensions.Logging;
using Web.Infrastructure.Decorators.Abstractions;

namespace Web.Infrastructure.Decorators.Implementations
{
    /// <summary>
    /// Декоратор логирования
    /// </summary>
    public class LoggingHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly ILogger<IHandler<TIn, TOut>> _logger;

        /// <inheritdoc />
        public LoggingHandlerDecorator(IHandler<TIn, TOut> decorated, ILogger<IHandler<TIn, TOut>> logger) : base(decorated)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public override TOut Handle(TIn input)
        {
            var result = base.Decoratee.Handle(input);
            _logger.LogInformation($"{base.Decoratee.GetType()} : {input} => {result}");
            return result;
        }
    }
}
