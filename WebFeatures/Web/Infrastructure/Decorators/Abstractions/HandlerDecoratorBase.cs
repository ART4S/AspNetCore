namespace Web.Infrastructure.Decorators.Abstractions
{
    /// <summary>
    /// Декоратор обработчика
    /// </summary>
    public abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, TOut>
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
