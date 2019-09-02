using Microsoft.EntityFrameworkCore;
using Web.Infrastructure.Decorators.Abstractions;

namespace Web.Infrastructure.Decorators.Implementations
{
    /// <summary>
    /// Сохраняет изменения контекста
    /// </summary>
    /// <remarks>Вызывается после работы всех декораторов для принятия изменений в рамках одной транзакции</remarks>
    public class SaveChangesHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly DbContext _dbContext;

        /// <inheritdoc />
        public SaveChangesHandlerDecorator(IHandler<TIn, TOut> decorated, DbContext dbContext) : base(decorated)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public override TOut Handle(TIn input)
        {
            var result = Decoratee.Handle(input);
            _dbContext.SaveChanges();
            return result;
        }
    }
}
