using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Обработчик команды
    /// </summary>
    /// <typeparam name="TCommand">Команда</typeparam>
    /// <typeparam name="TResult">Результат выполнения команды</typeparam>
    public interface ICommandHandler<in TCommand, out TResult> : IHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
