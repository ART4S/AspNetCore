using Microsoft.EntityFrameworkCore;
using Web.Decorators.Abstractions;

namespace Web.Decorators.Implementations
{
    /// <summary>
    /// Сохранет изменения контекста
    /// </summary>
    /// <remarks>Вызывается после работы всех декораторов для принятия изменений в рамках одной транзакции</remarks>
    public class SaveChangesDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly DbContext _dbContext;

        /// <inheritdoc />
        public SaveChangesDecorator(IHandler<TIn, TOut> decorated, DbContext dbContext) : base(decorated)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public override TOut Handle(TIn input)
        {
            var res = Decoratee.Handle(input);
            _dbContext.SaveChanges();
            return res;
        }
    }
}
