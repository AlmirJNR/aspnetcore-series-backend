namespace Contracts.Repositories.Interfaces;

public interface IRepository<T>
{
    public Task<T?> AddNew(T t);
    public Task<bool> DeleteAllIds(IEnumerable<Guid> ids);
    public Task<bool> DeleteById(Guid id);
    public Task<IEnumerable<T>> GetAllPaginated(bool includeDeleted, int page, int limit);
    public Task<T?> GetById(Guid id);
    public Task<bool> UpdateById(T t);
}