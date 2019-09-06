namespace Web.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Декоратор обработчика запросов
    /// </summary>
    public abstract class HandlerDecoratorBase<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        /// <summary>
        /// Декорируемый обработчик
        /// </summary>
        protected readonly IRequestHandler<TRequest, TResult> Decoratee;

        /// <inheritdoc />
        protected HandlerDecoratorBase(IRequestHandler<TRequest, TResult> decoratee)
        {
            Decoratee = decoratee;
        }

        /// <inheritdoc />
        public abstract TResult Handle(TRequest request);
    }
}
