﻿using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Pipeline.Implementations
{
    class PerformanceHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly ILogger<TIn> _logger;
        private readonly Stopwatch _timer = new Stopwatch();

        public PerformanceHandlerDecorator(ILogger<TIn> logger, IHandler<TIn, TOut> decoratee) : base(decoratee)
        {
            _logger = logger;
        }

        public override TOut Handle(TIn input)
        {
            _timer.Start();
            var result = Decoratee.Handle(input);
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Долгий запрос: {typeof(TIn).Name} = {_timer.ElapsedMilliseconds} milliseconds");
            }

            return result;
        }
    }
}
