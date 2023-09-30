using Microsoft.EntityFrameworkCore;
using Sample.WebApi.Contract;
using Sample.WebApi.Data;
using Sample.WebApi.Data.Models;

namespace Sample.WebApi.Services
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _appDbContext;
        public BrandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> Create(Brand entity)
        {
            await _appDbContext.Brands.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Brand entity)
        {
             _appDbContext.Brands.Remove(entity);
            return await Save();
        }

        public async Task<IList<Brand>> GetAll()
        {
         return   await _appDbContext.Brands.ToListAsync();
        }

        public async Task<Brand> GetById(int id)
        {
         return await _appDbContext.Brands.FindAsync(id);
        }

        public async Task<bool> Save()
        {
          var result=  _appDbContext.SaveChangesAsync();
            return await result > 0;
        }

        public async Task<bool> Update(Brand entity)
        {
            _appDbContext.Brands.Update(entity);
            return await Save();
        }
    }
}
