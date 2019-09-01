namespace Web.Decorators.Abstractions
{
    /// <summary>
    /// Декоратор обработчика
    /// </summary>
    public abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, TOut>
    {
        /// <summary>
        /// Декорируемый обработчик
        /// </summary>
        protected readonly IHandler<TIn, TOut> Decorated;

        /// <inheritdoc />
        protected HandlerDecoratorBase(IHandler<TIn, TOut> decorated)
        {
            Decorated = decorated;
        }

        /// <inheritdoc />
        public abstract TOut Handle(TIn input);
    }
}
