using Microsoft.EntityFrameworkCore;
using Web.Decorators.Abstractions;
using Web.Infrastructure;

namespace Web.Decorators.Implementations
{
    /// <summary>
    /// Сохранет изменения контекста (чтобы все изменения происходили в рамках одной транзакции)
    /// </summary>
    public class SaveChangesDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
        where TOut : Result
    {
        private readonly DbContext _dbContext;

        public SaveChangesDecorator(IHandler<TIn, TOut> decorated, DbContext dbContext) : base(decorated)
        {
            _dbContext = dbContext;
        }

        public override TOut Handle(TIn input)
        {
            var res = Decorated.Handle(input);
            _dbContext.SaveChanges();
            return res;
        }
    }
}
