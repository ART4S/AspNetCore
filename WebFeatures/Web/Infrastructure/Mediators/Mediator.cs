using System;
using System.Linq;
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

        public TOut Send<TIn, TOut>(TIn input)
        {
            Type typeDefinition = null;

            if (typeof(TIn).GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommand<>)))
            {
                typeDefinition = typeof(ICommandHandler<,>);
            }

            if (typeof(TIn).GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQuery<>)))
            {
                typeDefinition = typeof(IQueryHandler<,>);
            }

            if (typeDefinition == null)
            {
                throw new ArgumentException($"Invalid type: {typeof(TIn).Name}");
            }

            var handlerType = typeDefinition.MakeGenericType(typeof(TIn), typeof(TOut));
            dynamic handler = _serviceProvider.GetService(handlerType);

            return handler.Handle(input);
        }
    }
}
