namespace Sample.WebApi.Contract
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();


    }
}
