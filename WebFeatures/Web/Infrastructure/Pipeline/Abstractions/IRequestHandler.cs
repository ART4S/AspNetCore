namespace Web.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Обработчик запросов
    /// </summary>
    /// <typeparam name="TRequest">Запрос</typeparam>
    /// <typeparam name="TResult">Результат выполнения запроса</typeparam>
    public interface IRequestHandler<in TRequest, out TResult> where TRequest : IRequest<TResult>
    {
        /// <summary>
        /// Метод обработки запроса
        /// </summary>
        /// <param name="request">Запрос</param>
        TResult Handle(TRequest request);
    }
}
