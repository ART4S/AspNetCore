using System;
using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Mediators
{
    class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult SendQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            return Send<TQuery, TResult>(query, typeof(IQueryHandler<,>));
        }

        public TResult SendCommand<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            return Send<TCommand, TResult>(command, typeof(ICommandHandler<,>));
        }

        private TOut Send<TIn, TOut>(TIn input, Type handlerTypeDefinition)
        {
            var handlerType = handlerTypeDefinition.MakeGenericType(typeof(TIn), typeof(TOut));
            dynamic handler = _serviceProvider.GetService(handlerType);

            return handler.Handle(input);
        }
    }
}
