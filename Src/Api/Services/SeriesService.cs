using Contracts.Repositories.Interfaces;
using Contracts.SeriesContracts.Dto;
using Contracts.SeriesContracts.Interfaces;
using Data.Models;
using SeriesBackend.Repositories;

namespace SeriesBackend.Services;

public class SeriesService : ISeriesService
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly ICategoryRepository _categoriesRepository;

    public SeriesService(ISeriesRepository seriesRepository, ICategoryRepository categoriesRepository)
    {
        _seriesRepository = seriesRepository;
        _categoriesRepository = categoriesRepository;
    }

    public Task<Series?> AddNew(CreateSeriesDto createSeriesDto)
        => _seriesRepository.AddNew(createSeriesDto.ToModel());

    public Task<bool> DeleteAllIds(IEnumerable<Guid> ids) => _seriesRepository.DeleteAllIds(ids);

    public Task<bool> DeleteById(Guid id) => _seriesRepository.DeleteById(id);

    public async Task<IEnumerable<ReducedSeriesDto>> GetAllPaginated(bool includeDeleted, int page, int limit)
    {
        var response =
            await _seriesRepository.GetAllPaginated(includeDeleted: includeDeleted, page: page, limit: limit);

        return response.Select(ReducedSeriesDto.FromModel);
    }

    public Task<Series?> GetById(Guid id) => _seriesRepository.GetById(id);
    
    public async Task<bool> InsertNewCategory(Guid seriesId, Guid categoryId)
    {
        var categoryModel = await _categoriesRepository.GetById(categoryId);

        if (categoryModel is null)
        {
            return false;
        }
        
        return await _seriesRepository.InsertNewCategory(seriesId, categoryModel);
    }

    public Task<bool> UpdateById(Guid id, UpdateSeriesDto updateSeriesDto)
    {
        var seriesModel = updateSeriesDto.ToModel();
        seriesModel.Id = id;
        
        return _seriesRepository.UpdateById(seriesModel);
    }
}