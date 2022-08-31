using Contracts.Categories.Dto;
using Data.Models;

namespace Contracts.Categories.Interfaces;

public interface ICategoryService
{
    public Task<Category?> AddNew(CreateCategoryDto createCategoryDto);
    public Task<bool> DeleteAllIds(IEnumerable<Guid> ids);
    public Task<bool> DeleteById(Guid id);
    public Task<IEnumerable<ReducedCategoryDto>> GetAllPaginated(bool includeDeleted, int page, int limit);
    public Task<Category?> GetById(Guid id);
    public Task<bool> UpdateById(Guid id, UpdateCategoryDto updateCategoryDto);
}