using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Pipeline.Implementations
{
    class PerformanceHandlerDecorator<TRequest, TResult> : HandlerDecoratorBase<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer = new Stopwatch();

        public PerformanceHandlerDecorator(ILogger<TRequest> logger, IRequestHandler<TRequest, TResult> decoratee) : base(decoratee)
        {
            _logger = logger;
        }

        public override TResult Handle(TRequest request)
        {
            _timer.Start();
            var result = Decoratee.Handle(request);
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Долгий запрос: {typeof(TRequest).Name} = {_timer.ElapsedMilliseconds} milliseconds");
            }

            return result;
        }
    }
}
