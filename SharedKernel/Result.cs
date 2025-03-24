namespace HogwartsAPI.SharedKernel
{

    public class Result
    {
        private Result(Error error)
        {
            IsSuccess = false;
            Error = error;
            Success = Success.None;
        }

        private Result(Success success)
        {
            IsSuccess = true;
            Error = Error.None;
            Success = success;
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public Success Success { get; }
        public static Result Ok(Success success) => new(success);
        public static Result Failure(Error error) => new(error);

    }
}