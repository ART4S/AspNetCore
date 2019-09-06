using System;
using Web.Infrastructure.Results;

namespace Web.Infrastructure.Exceptions
{
    /// <summary>
    /// Исключение с результатом
    /// </summary>
    class ResultException : Exception
    {
        public Result Result { get; }

        public ResultException(Result result)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
        }
    }
}
