using System;
using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Mediators
{
    public class RequestMediator : IRequestMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResponse Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(typeof(TRequest), typeof(TResponse));
            dynamic handler = _serviceProvider.GetService(handlerType);

            return handler.Handle(request);
        }
    }
}
