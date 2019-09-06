using System;

namespace Web.Infrastructure.Failures
{
    /// <summary>
    /// Ошибка запроса
    /// </summary>
    public class Fail
    {
        /// <summary>
        /// Сообщение с информацией об ошибке
        /// </summary>
        public string Message { get; }

        /// <inheritdoc />
        public Fail(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        /// <summary>
        /// Преобразует строку в ошибку
        /// </summary>
        public static implicit operator Fail(string str)
        {
            return new Fail(str);
        }
    }
}
