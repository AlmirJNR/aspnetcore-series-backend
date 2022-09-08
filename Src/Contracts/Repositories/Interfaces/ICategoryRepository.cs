using Data.Models;

namespace Contracts.Repositories.Interfaces;

public interface ICategoryRepository
{
    public Task<Category?> AddNew(Category categoryModel);
    public Task<bool> DeleteAllIds(IEnumerable<Guid> ids);
    public Task<bool> DeleteById(Guid id);
    public Task<IEnumerable<Category>> GetAllPaginated(bool includeDeleted, int page, int limit);
    public Task<Category?> GetById(Guid id);
    public Task<bool> UpdateById(Category categoryModel);
}