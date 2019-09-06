using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Mediators
{
    public interface IRequestMediator
    {
        TResponse Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
    }
}
