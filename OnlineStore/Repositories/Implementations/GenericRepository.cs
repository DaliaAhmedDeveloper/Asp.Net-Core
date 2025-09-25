namespace OnlineStore.Repositories;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public virtual async Task<IEnumerable<T>> GetLatestAsync()
    {
        return await _dbSet.OrderByDescending(e => EF.Property<DateTime>(e, "CreatedAt")).Take(4).ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    public virtual async Task<bool> ExistsAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }
    // count all 
    public virtual async Task<int> CountAllAsync()
    {
        return await _dbSet.CountAsync();
    }
      // count current month
    public virtual async Task<int> CountCurrentMonthAsync()
    {
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;
        return await _dbSet
        .Where(e => EF.Property<DateTime>(e, "CreatedAt").Month == currentMonth
        && EF.Property<DateTime>(e, "CreatedAt").Year == currentYear).CountAsync();
    }

    // get all with translations pagination 

    public virtual async Task<IEnumerable<T>> GetAllWithTranslationsAndPaginationAsync(int pageNumber = 1, int pageSize = 10)
    {
        return await _dbSet.Include("Translations").Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    // get with translations
    public virtual async Task<T?> GetWithTranslationsAsync(int id)
    {
        return await _dbSet.Include("Translations").Where(e => EF.Property<int>(e, "Id") == id).FirstOrDefaultAsync();
    }
    // get all with translations
    public virtual async Task<IEnumerable<T>> GetAllWithTranslationsAsync()
    {
        return await _dbSet.Include("Translations").ToListAsync();
    }
}