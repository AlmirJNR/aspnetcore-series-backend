using Contracts;
using Contracts.Repositories.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace SeriesBackend.Repositories;

public class SeriesRepository : IRepository<Series>
{
    private readonly PostgresContext _postgresContext;
    private readonly DbSet<Series> _seriesEntity;

    public SeriesRepository(PostgresContext dbContext)
    {
        _postgresContext = dbContext;
        _seriesEntity = _postgresContext.Series;
    }

    public async Task<Series?> AddNew(Series series)
    {
        var seriesEntity = await _seriesEntity.AddAsync(series);
        var response = await _postgresContext.SaveChangesAsync();

        return response == 1 ? seriesEntity.Entity : null;
    }

    public async Task<bool> DeleteAllIds(IEnumerable<Guid> ids)
    {
        var idsToDelete = await _seriesEntity
            .Where(series => ids.Contains(series.Id))
            .ToListAsync();

        var idsEnumerable = ids as Guid[] ?? ids.ToArray();
        
        if (idsToDelete.Count != idsEnumerable.Length)
        {
            return false;
        }

        foreach (var series in idsToDelete)
        {
            series.DeletedAt = DateTime.Now;
        }

        _seriesEntity.UpdateRange(idsToDelete);
        var response = await _postgresContext.SaveChangesAsync();

        return response == idsEnumerable.Length;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var seriesToDelete = await _seriesEntity.FirstOrDefaultAsync(series => series.Id == id);

        if (seriesToDelete is null)
        {
            return false;
        }

        seriesToDelete.DeletedAt = DateTime.Now;

        _seriesEntity.Update(seriesToDelete);
        var response = await _postgresContext.SaveChangesAsync();

        return response == 1;
    }

    public async Task<IEnumerable<Series>> GetAllPaginated(bool includeDeleted, int page, int limit)
    {
        if (includeDeleted)
        {
            return await _seriesEntity
                .Where(value => value.DeletedAt != null)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        return await _seriesEntity
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }

    public Task<Series?> GetById(Guid id) => _seriesEntity.FirstOrDefaultAsync(series => series.Id == id);

    public async Task<bool> InsertNewCategory(Guid seriesId, Category categoryModel)
    {
        var series = await _seriesEntity.FirstOrDefaultAsync(series => series.Id == seriesId);

        if (series is null)
        {
            return false;
        }
        
        series.Categories.Add(categoryModel);
        var response = await _postgresContext.SaveChangesAsync();
        
        return response == 1;
    }

    public async Task<bool> UpdateById(Series series)
    {
        _seriesEntity.Update(series);
        var response = await _postgresContext.SaveChangesAsync();

        return response == 1;
    }
}