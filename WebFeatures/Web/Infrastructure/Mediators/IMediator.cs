using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Mediators
{
    /// <summary>
    /// Посредник для отправки команд/запросов подходящим обработчикам
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Послать запрос обработчику
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Результат выполнения запроса</returns>
        TResponse SendQuery<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;

        /// <summary>
        /// Послать команду обработчику
        /// </summary>
        /// <param name="command">Команда</param>
        /// <returns>Результат выполнения команды</returns>
        TResult SendCommand<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
    }
}
