namespace HogwartsAPI.SharedKernel
{

    public interface IResult
    {
        int StatusCode { get; }
        string Message { get; }
    }
}