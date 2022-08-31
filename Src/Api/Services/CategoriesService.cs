using Contracts.Categories.Dto;
using Contracts.Categories.Interfaces;
using Data.Models;
using SeriesBackend.Repositories;

namespace SeriesBackend.Services;

public class CategoriesService : ICategoryService
{
    private readonly CategoriesRepository _categoriesRepository;

    public CategoriesService(CategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public Task<Category?> AddNew(CreateCategoryDto createCategoryDto)
        => _categoriesRepository.AddNew(createCategoryDto.ToModel());

    public Task<bool> DeleteAllIds(IEnumerable<Guid> ids) => _categoriesRepository.DeleteAllIds(ids);
    
    public Task<bool> DeleteById(Guid id) => _categoriesRepository.DeleteById(id);

    public async Task<IEnumerable<ReducedCategoryDto>> GetAllPaginated(bool includeDeleted, int page, int limit)
    {
        var response =
            await _categoriesRepository.GetAllPaginated(includeDeleted: includeDeleted, page: page, limit: limit);

        return response.Select(ReducedCategoryDto.FromModel);
    }

    public Task<Category?> GetById(Guid id) => _categoriesRepository.GetById(id);

    public Task<bool> UpdateById(Guid id, UpdateCategoryDto updateCategoryDto)
    {
        var categoryModel = updateCategoryDto.ToModel();
        categoryModel.Id = id;
        
        return _categoriesRepository.UpdateById(categoryModel);
    }
}