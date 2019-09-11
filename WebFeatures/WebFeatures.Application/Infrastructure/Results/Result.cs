using System.Collections.Generic;

namespace WebFeatures.Application.Infrastructure.Results
{
    /// <summary>
    /// Типизированный результат выполнения команды/запроса
    /// </summary>
    /// <typeparam name="TSuccess">Значение в случае успеха выполнения</typeparam>
    /// <typeparam name="TFailure">Значение в случае неудачного выполнения</typeparam>
    public sealed class Result<TSuccess, TFailure>
    {
        public int StatusCode { get; }
        public TSuccess SuccessValue { get; }
        public TFailure FailureValue { get; }
        public bool IsSuccess => EqualityComparer<TFailure>.Default.Equals(FailureValue, default);

        private Result(TSuccess success, int statusCode)
        {
            StatusCode = statusCode;
            SuccessValue = success;
        }

        private Result(TFailure failure, int statusCode)
        {
            StatusCode = statusCode;
            FailureValue = failure;
        }

        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="success">Значение успешного результата</param>
        /// <param name="statusCode">Код</param>
        public static Result<TSuccess, TFailure> Success(TSuccess success = default, int statusCode = 200)
            => new Result<TSuccess, TFailure>(success, statusCode);

        /// <summary>
        /// Неудачный результат
        /// </summary>
        /// <param name="failure">Значение неудачного результата</param>
        /// <param name="statusCode">Код</param>
        public static Result<TSuccess, TFailure> Fail(TFailure failure = default, int statusCode = 400)
            => new Result<TSuccess, TFailure>(failure, statusCode);
    }

    /// <summary>
    /// Результат выполнения команды
    /// </summary>
    /// <remarks>Используется как альтернатива void</remarks>
    public sealed class Result
    {
        public int StatusCode { get; }
        public object FailureValue { get; }
        public bool IsSuccess => FailureValue == null;

        private Result(int statusCode)
        {
            StatusCode = statusCode;
        }

        private Result(object failure, int statusCode)
        {
            StatusCode = statusCode;
            FailureValue = failure;
        }

        public static Result Success(int statusCode = 200)
            => new Result(statusCode);

        public static Result Fail(object failure, int statusCode = 400)
            => new Result(failure, statusCode);
    }
}