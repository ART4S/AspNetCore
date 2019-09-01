using Microsoft.Extensions.Logging;
using Web.Decorators.Abstractions;

namespace Web.Decorators.Implementations
{
    /// <summary>
    /// Декоратор логирования
    /// </summary>
    public class LoggerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly ILogger<IHandler<TIn, TOut>> _logger;

        /// <inheritdoc />
        public LoggerDecorator(IHandler<TIn, TOut> decorated, ILogger<IHandler<TIn, TOut>> logger) : base(decorated)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public override TOut Handle(TIn input)
        {
            var result = Decoratee.Handle(input);
            _logger.LogInformation($"{Decoratee.GetType()} : {input} => {result}");
            return result;
        }
    }
}
