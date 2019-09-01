namespace Web.Infrastructure.Decorators.Abstractions
{
    /// <summary>
    /// Запрос
    /// </summary>
    /// <typeparam name="TOut">Результат выполнения запроса</typeparam>
    public interface IQuery<out TOut>
    {
    }
}
