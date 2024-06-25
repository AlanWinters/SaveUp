using Microsoft.EntityFrameworkCore;
using SaveUp.Models;

namespace SaveUp.Library.Services
{
    public class ProductService(AppDbContext dbContext) : BaseService<ProductModel>(dbContext)
    {
        public override async Task<ProductModel?> CreateAsync(ProductModel t)
        {
            DbContext.Products.Add(t);

            return await DbContext.SaveChangesAsync() > 0 ? t : null;
        }

        public override async Task<bool> DeleteAllAsync()
        {
            DbContext.Products.RemoveRange(DbContext.Products);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public override async Task<bool> DeleteAsync(ProductModel t)
        {
            DbContext.Products.Remove(t);
            return (await DbContext.SaveChangesAsync()) > 0;
        }

        public override async Task<List<ProductModel>> GetAllAsync()
        {
            return await DbContext.Products.ToListAsync();
        }

        public override async Task<ProductModel?> GetSingleAsync(int id)
        {
            return await DbContext.Products.FindAsync(id);
        }

        public override async Task<bool> UpdateAsync(ProductModel t)
        {
            DbContext.Products.Update(t);

            return (await DbContext.SaveChangesAsync()) > 0;
        }
    }
}
