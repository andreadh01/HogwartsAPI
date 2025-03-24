using HogwartsAPI.Web.Api.Constants;
namespace HogwartsAPI.SharedKernel
{
    public record Error : IResult
    {
        public static implicit operator Result(Error error) => Result.Failure(error);
        public static readonly Error None = new(0, string.Empty);

        private Error(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public int StatusCode { get; }
        public string Message { get; }

        public static Error NotFound(string message = ResponseMessages.NotFound) => new(StatusCodes.Status404NotFound, message);
        public static Error Validation(string message = ResponseMessages.BadRequest) => new(StatusCodes.Status400BadRequest, message);
        public static Error Conflict(string message = ResponseMessages.Conflict) => new(StatusCodes.Status409Conflict, message);
        public static Error Failure(string message = ResponseMessages.ServerError) => new(StatusCodes.Status500InternalServerError, message);
        public static Error NotImplemented(string message = ResponseMessages.ServerError) => new(StatusCodes.Status501NotImplemented, message);

    }
}