namespace Web.Infrastructure.Decorators.Abstractions
{
    /// <summary>
    /// Команда
    /// </summary>
    /// <typeparam name="TOut">Результат выполнения команды</typeparam>
    public interface ICommand<out TOut>
    {
    }
}
