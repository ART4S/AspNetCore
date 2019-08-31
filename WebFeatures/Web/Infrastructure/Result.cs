namespace Web.Infrastructure
{
    public class Result
    {
        public int StatusCode { get; set; }

        public object SuccessValue { get; set; }

        public object FailureValue { get; set; }

        public bool IsSuccess { get; set; }

        protected Result() { }

        public static Result Success(object success = null, int statusCode = 200)
        {
            return new Result
            {
                StatusCode = statusCode,
                SuccessValue = success,
                IsSuccess = true
            };
        }

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

    public sealed class Result<TSuccess, TFailure> : Result
    {
        public new TSuccess SuccessValue
        {
            get => (TSuccess) base.SuccessValue;
            set => base.SuccessValue = value;
        }

        public new TFailure FailureValue
        {
            get => (TFailure)base.FailureValue;
            set => base.FailureValue = value;
        }

        public static Result<TSuccess, TFailure> Success(TSuccess success = default, int statusCode = 200)
        {
            return new Result<TSuccess, TFailure>
            {
                StatusCode = statusCode,
                SuccessValue = success
            };
        }

        public static Result<TSuccess, TFailure> Fail(TFailure failure = default, int statusCode = 400)
        {
            return new Result<TSuccess, TFailure>
            {
                StatusCode = statusCode,
                FailureValue = failure
            };
        }

        //public TResult Match<TResult>(Func<TSuccess, TResult> successVisitor, Func<TFailure, TResult> failureVisitor)
        //    => IsSuccess ? successVisitor(SuccessValue) : failureVisitor(FailureValue);
    }
}
