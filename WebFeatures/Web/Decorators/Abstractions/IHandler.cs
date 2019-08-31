using Web.Infrastructure;

namespace Web.Decorators.Abstractions
{
    /// <summary>
    /// Обработчик
    /// </summary>
    public interface IHandler<in TIn, out TOut> where TOut : Result
    {
        TOut Handle(TIn input);
    }
}
