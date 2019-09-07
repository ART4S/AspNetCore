using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Команда
    /// </summary>
    /// <typeparam name="TOut">Результат выполнения команды</typeparam>
    public interface ICommand<TOut>
    {
    }
}
