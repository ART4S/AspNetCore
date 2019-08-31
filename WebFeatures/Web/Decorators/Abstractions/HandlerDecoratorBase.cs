using Web.Infrastructure;

namespace Web.Decorators.Abstractions
{
    public abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, TOut>
        where TOut : Result
    {
        protected readonly IHandler<TIn, TOut> Decorated;

        protected HandlerDecoratorBase(IHandler<TIn, TOut> decorated)
        {
            Decorated = decorated;
        }

        public abstract TOut Handle(TIn input);
    }
}
