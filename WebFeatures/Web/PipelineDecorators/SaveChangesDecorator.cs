using Microsoft.EntityFrameworkCore;
using Web.Abstractions;

namespace Web.PipelineDecorators
{
    public class SaveChangesDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
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
