using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly AppDbContext _appDbContext;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<List<TModel>> Get()
        {
            return _appDbContext.Set<TModel>().ToListAsync();
        }

        public Task<TModel> Get(int id)
        {
            return _appDbContext.Set<TModel>().FindAsync(id).AsTask();
        }
        public void Create(TModel model)
        {
            _appDbContext.Set<TModel>().Add(model);
        }

        public void Delete(TModel model)
        {
            _appDbContext.Set<TModel>().Remove(model);
        }

        public void Update(TModel model)
        {
            _appDbContext.Set<TModel>().Update(model);
        }

        public void CreateRange(IEnumerable<TModel> models)
        {
            _appDbContext.Set<TModel>().AddRange(models);
        }

        public void UpdateRange(IEnumerable<TModel> models)
        {
            _appDbContext.Set<TModel>().UpdateRange(models);
        }

        public void DeleteRange(IEnumerable<TModel> models)
        {
            _appDbContext.Set<TModel>().RemoveRange(models);
        }
    }
}
