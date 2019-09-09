namespace WebFeatures.Application.Pipeline.Mediators
{
    /// <summary>
    /// Посредник для отправки запросов подходящим обработчикам
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Послать запрос обработчику
        /// </summary>
        /// <param name="input">Данные запроса</param>
        /// <returns>Результат выполнения запроса</returns>
        TOut Send<TIn, TOut>(TIn input);
    }
}
