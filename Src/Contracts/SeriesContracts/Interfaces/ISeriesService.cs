using Contracts.SeriesContracts.Dto;
using Data.Models;

namespace Contracts.SeriesContracts.Interfaces;

public interface ISeriesService
{
    public Task<Series?> AddNew(CreateSeriesDto createSeriesDto);
    public Task<bool> DeleteAllIds(IEnumerable<Guid> ids);
    public Task<bool> DeleteById(Guid id);
    public Task<bool> InsertNewCategory(Guid seriesId, Guid categoryId);
    public Task<IEnumerable<ReducedSeriesDto>> GetAllPaginated(bool includeDeleted, int page, int limit);
    public Task<Series?> GetById(Guid id);
    public Task<bool> UpdateById(Guid id, UpdateSeriesDto updateSeriesDto);
}