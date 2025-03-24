namespace HogwartsAPI.SharedKernel
{

    public interface IRepository<T> where T : class
    {
        Result Create(T data);
        Result Update(T data);
        Result Delete(Guid id);
        Result Get(Guid id);

        Result GetAll();

    }
}