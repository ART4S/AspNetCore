namespace Web.Abstractions
{
    interface IQueryHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn : IQuery<TOut>
    {
    }
}
