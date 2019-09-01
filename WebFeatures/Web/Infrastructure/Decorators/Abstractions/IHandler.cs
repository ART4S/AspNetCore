namespace Web.Infrastructure.Decorators.Abstractions
{
    /// <summary>
    /// Обработчик
    /// </summary>
    public interface IHandler<in TIn, out TOut>
    {
        /// <summary>
        /// Метод обработки
        /// </summary>
        /// <param name="input">Данные для обработки</param>
        TOut Handle(TIn input);
    }
}
