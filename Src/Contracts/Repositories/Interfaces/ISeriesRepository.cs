using Data.Models;

namespace Contracts.Repositories.Interfaces;

public interface ISeriesRepository
{
    public Task<Series?> AddNew(Series seriesModel);
    public Task<bool> DeleteAllIds(IEnumerable<Guid> ids);
    public Task<bool> DeleteById(Guid id);
    public Task<IEnumerable<Series>> GetAllPaginated(bool includeDeleted, int page, int limit);
    public Task<Series?> GetById(Guid id);
    public Task<bool> InsertNewCategory(Guid seriesId, Category categoryModel);
    public Task<bool> UpdateById(Series seriesModel);
}