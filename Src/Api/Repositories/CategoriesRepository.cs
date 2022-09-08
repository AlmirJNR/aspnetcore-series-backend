using Contracts;
using Contracts.Repositories.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace SeriesBackend.Repositories;

public class CategoriesRepository : ICategoryRepository
{
    private readonly PostgresContext _postgresContext;
    private readonly DbSet<Category> _categoriesEntity;

    public CategoriesRepository(PostgresContext dbContext)
    {
        _postgresContext = dbContext;
        _categoriesEntity = _postgresContext.Categories;
    }

    public async Task<Category?> AddNew(Category category)
    {
        var categoryEntity = await _categoriesEntity.AddAsync(category);
        var response = await _postgresContext.SaveChangesAsync();

        return response == 1 ? categoryEntity.Entity : null;
    }

    public async Task<bool> DeleteAllIds(IEnumerable<Guid> ids)
    {
        var idsToDelete = await _categoriesEntity
            .Where(category => ids.Contains(category.Id))
            .ToListAsync();

        var idsEnumerable = ids as Guid[] ?? ids.ToArray();

        if (idsToDelete.Count != idsEnumerable.Length)
        {
            return false;
        }

        foreach (var category in idsToDelete)
        {
            category.DeletedAt = DateTime.Now;
        }

        _postgresContext.UpdateRange(idsToDelete);
        var response = await _postgresContext.SaveChangesAsync();

        return response == idsEnumerable.Length;
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var idToDelete = await _categoriesEntity.FirstOrDefaultAsync(category => category.Id == id);

        if (idToDelete is null)
        {
            return false;
        }

        idToDelete.DeletedAt = DateTime.Now;

        _postgresContext.Update(idToDelete);
        var response = await _postgresContext.SaveChangesAsync();

        return response == 1;
    }

    public async Task<IEnumerable<Category>> GetAllPaginated(bool includeDeleted, int page, int limit)
    {
        if (includeDeleted)
        {
            return await _categoriesEntity
                .Where(category => category.DeletedAt != null)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        return await _categoriesEntity
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }

    public Task<Category?> GetById(Guid id) => _categoriesEntity.FirstOrDefaultAsync(category => category.Id == id);

    public async Task<bool> UpdateById(Category category)
    {
        _categoriesEntity.Update(category);
        var response = await _postgresContext.SaveChangesAsync();

        return response == 1;
    }
}