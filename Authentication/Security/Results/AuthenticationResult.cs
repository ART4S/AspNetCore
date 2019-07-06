using Security.Dto;
using System;

namespace Security.Results
{
    /// <summary>
    /// Результат аутентификации
    /// </summary>
    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }

        public AccountDto Account { get; }

        public string Message { get; }

        public Exception Exception { get; }

        private AuthenticationResult(string message, Exception exception)
        {
            IsSuccess = false;
            Message = message;
            Exception = exception;
        }

        private AuthenticationResult(AccountDto account)
        {
            IsSuccess = true;
            Account = account;
        }

        /// <summary>
        /// Успешный результат аутентификации
        /// </summary>
        public static AuthenticationResult Success(AccountDto account)
        {
            return new AuthenticationResult(account);
        }

        /// <summary>
        /// Ошибка аутентификации
        /// </summary>
        public static AuthenticationResult Fail(string message, Exception exception = null)
        {
            return new AuthenticationResult(message, exception);
        }
    }
}
