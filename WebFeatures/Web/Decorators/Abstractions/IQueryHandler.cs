using Web.Infrastructure;

namespace Web.Decorators.Abstractions
{
    /// <summary>
    /// Обработчик запроса
    /// </summary>
    /// <typeparam name="TIn">Запрос</typeparam>
    /// <typeparam name="TOut" >Результат выполнения запроса</typeparam>
    interface IQueryHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn : IQuery<TOut>
        where TOut : Result
    {
    }
}
