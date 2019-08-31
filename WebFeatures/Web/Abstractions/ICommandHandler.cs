namespace Web.Abstractions
{
    interface ICommandHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn : ICommand<TOut>
    {
    }
}
