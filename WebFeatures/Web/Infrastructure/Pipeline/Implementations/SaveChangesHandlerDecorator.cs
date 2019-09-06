using Microsoft.EntityFrameworkCore;
using Web.Infrastructure.Pipeline.Abstractions;

namespace Web.Infrastructure.Pipeline.Implementations
{
    /// <summary>
    /// Сохраняет изменения контекста
    /// </summary>
    /// <remarks>Вызывается после работы всех декораторов для принятия изменений в рамках одной транзакции</remarks>
    class SaveChangesHandlerDecorator<TRequest, TResult> : HandlerDecoratorBase<TRequest, TResult> 
        where TRequest : IRequest<TResult>
    {
        private readonly DbContext _dbContext;

        /// <inheritdoc />
        public SaveChangesHandlerDecorator(IRequestHandler<TRequest, TResult> decorated, DbContext dbContext) : base(decorated)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public override TResult Handle(TRequest request)
        {
            var result = Decoratee.Handle(request);
            _dbContext.SaveChanges();
            return result;
        }
    }
}
