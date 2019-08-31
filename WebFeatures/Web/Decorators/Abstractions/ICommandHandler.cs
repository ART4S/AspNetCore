using Web.Infrastructure;

namespace Web.Decorators.Abstractions
{
    /// <summary>
    /// Обработчик команд
    /// </summary>
    /// <typeparam name="TIn">Команда</typeparam>
    /// <typeparam name="TOut">Результат выполнения команды</typeparam>
    interface ICommandHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn : ICommand<TOut>
        where TOut : Result
    {
    }
}
