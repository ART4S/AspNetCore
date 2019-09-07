namespace Web.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Декоратор для обработчика
    /// </summary>
    abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, TOut>
    {
        /// <summary>
        /// Декорируемый обработчик
        /// </summary>
        protected readonly IHandler<TIn, TOut> Decoratee;

        /// <inheritdoc />
        protected HandlerDecoratorBase(IHandler<TIn, TOut> decoratee)
        {
            Decoratee = decoratee;
        }

        /// <inheritdoc />
        public abstract TOut Handle(TIn input);
    }
}
