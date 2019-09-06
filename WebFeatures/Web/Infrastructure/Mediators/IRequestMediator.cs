using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Mediators
{
    /// <summary>
    /// Посредник для отправки запроса подходящим обработчикам
    /// </summary>
    public interface IRequestMediator
    {
        /// <summary>
        /// Послать запрос
        /// </summary>
        TResponse Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
    }
}
