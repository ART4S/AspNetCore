using Web.Infrastructure;

namespace Web.Decorators.Abstractions
{
    /// <summary>
    /// Команда
    /// </summary>
    /// <typeparam name="TOut">Результат выполнения команды</typeparam>
    public interface ICommand<out TOut> where TOut : Result
    {
    }
}
