namespace Catridges.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);

    Task<T> Read(int id);
    
    Task<List<T>> ReadAll();

    Task<bool> Update(int id, T entity);

    Task<bool> Delete(T entity);
}