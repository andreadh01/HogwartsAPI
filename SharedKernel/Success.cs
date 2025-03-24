using HogwartsAPI.Web.Api.Constants;
namespace HogwartsAPI.SharedKernel
{
    public record Success : IResult
    {
        public static implicit operator Result(Success success) => Result.Ok(success);

        public static readonly Success None = new(0, string.Empty);
        private Success(int statusCode, string message, object? data = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        public int StatusCode { get; }
        public string Message { get; }
        public object? Data { get; }

        public static Success Create(object data) => new(StatusCodes.Status201Created, ResponseMessages.Created, data);
        public static Success Update(object data) => new(StatusCodes.Status200OK, ResponseMessages.Updated, data);
        public static Success Delete => new(StatusCodes.Status200OK, ResponseMessages.Deleted);
        public static Success Query(object data) => new(StatusCodes.Status200OK, ResponseMessages.Success, data);
    }
}