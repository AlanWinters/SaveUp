using SaveUp.Models;

namespace SaveUp.Library.Services
{
    public abstract class BaseService<T>(AppDbContext dbContext) where T : BaseModel
    {
        public AppDbContext DbContext { get; } = dbContext;

        public abstract Task<List<T>> GetAllAsync(); // R
        public abstract Task<T?> CreateAsync(T t); // C
        public abstract Task<T?> GetSingleAsync(int id); // R
        public abstract Task<bool> UpdateAsync(T t); // U
        public abstract Task<bool> DeleteAsync(T t); // D
        public abstract Task<bool> DeleteAllAsync(); // D
    }
}
