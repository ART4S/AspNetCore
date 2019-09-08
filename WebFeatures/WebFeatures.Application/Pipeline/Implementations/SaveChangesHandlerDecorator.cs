﻿using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;

namespace WebFeatures.Application.Pipeline.Implementations
{
    /// <summary>
    /// Сохранение изменений контекста
    /// </summary>
    /// <remarks>Вызывается после работы всех декораторов для принятия изменений в рамках одной транзакции</remarks>
    public class SaveChangesHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly IAppContext _context;

        public SaveChangesHandlerDecorator(IHandler<TIn, TOut> decorated, IAppContext context) : base(decorated)
        {
            _context = context;
        }

        public override TOut Handle(TIn input)
        {
            var result = Decoratee.Handle(input);
            _context.SaveChanges();
            return result;
        }
    }
}