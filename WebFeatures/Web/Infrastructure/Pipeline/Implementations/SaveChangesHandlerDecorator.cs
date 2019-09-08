using DataContext;
using Web.Infrastructure.Pipeline.Abstractions;
using WebFeatures.DataContext;

namespace Web.Infrastructure.Pipeline.Implementations
{
    /// <summary>
    /// Сохраняет изменения контекста
    /// </summary>
    /// <remarks>Вызывается после работы всех декораторов для принятия изменений в рамках одной транзакции</remarks>
    class SaveChangesHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly IAppContext _context;

        /// <inheritdoc />
        public SaveChangesHandlerDecorator(IHandler<TIn, TOut> decorated, IAppContext context) : base(decorated)
        {
            _context = context;
        }

        /// <inheritdoc />
        public override TOut Handle(TIn input)
        {
            var result = Decoratee.Handle(input);
            _context.SaveChanges();
            return result;
        }
    }
}
