namespace WebFeatures.Application.Infrastructure.Results
{
    /// <summary>
    /// Результат выполнения запроса
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Код
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Значение успешного результата
        /// </summary>
        public object SuccessValue { get; set; }

        /// <summary>
        /// Значение неудачного результата
        /// </summary>
        public object FailureValue { get; set; }

        /// <summary>
        /// Индикатор успешного результата
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <inheritdoc />
        protected Result() { }

        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="success">Значение успешного результата</param>
        /// <param name="statusCode">Код</param>
        public static Result Success(object success = null, int statusCode = 200)
        {
            return new Result
            {
                StatusCode = statusCode,
                SuccessValue = success,
                IsSuccess = true
            };
        }

        /// <summary>
        /// Неудачный результат
        /// </summary>
        /// <param name="failure">Значение неудачного результата</param>
        /// <param name="statusCode">Код</param>
        public static Result Fail(object failure = null, int statusCode = 400)
        {
            return new Result
            {
                StatusCode = statusCode,
                FailureValue = failure,
                IsSuccess = false
            };
        }
    }

    /// <summary>
    /// Типизированный результат выполнения команды/запроса
    /// </summary>
    /// <typeparam name="TSuccess">Значение в случае успеха выполнения</typeparam>
    /// <typeparam name="TFailure">Значение в случае неудачного выполнения</typeparam>
    public sealed class Result<TSuccess, TFailure> : Result
    {
        /// <see cref="Result.SuccessValue"/>
        public new TSuccess SuccessValue
        {
            get => (TSuccess)base.SuccessValue;
            set => base.SuccessValue = value;
        }

        /// <see cref="Result.FailureValue"/>
        public new TFailure FailureValue
        {
            get => (TFailure)base.FailureValue;
            set => base.FailureValue = value;
        }

        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="success">Значение успешного результата</param>
        /// <param name="statusCode">Код</param>
        public static Result<TSuccess, TFailure> Success(TSuccess success = default, int statusCode = 200)
        {
            return new Result<TSuccess, TFailure>
            {
                StatusCode = statusCode,
                SuccessValue = success,
                IsSuccess = true
            };
        }

        /// <summary>
        /// Неудачный результат
        /// </summary>
        /// <param name="failure">Значение неудачного результата</param>
        /// <param name="statusCode">Код</param>
        public static Result<TSuccess, TFailure> Fail(TFailure failure = default, int statusCode = 400)
        {
            return new Result<TSuccess, TFailure>
            {
                StatusCode = statusCode,
                FailureValue = failure,
                IsSuccess = false
            };
        }

        //public TResult Match<TResult>(Func<TSuccess, TResult> successVisitor, Func<TFailure, TResult> failureVisitor)
        //    => IsSuccess ? successVisitor(SuccessValue) : failureVisitor(FailureValue);
    }
}