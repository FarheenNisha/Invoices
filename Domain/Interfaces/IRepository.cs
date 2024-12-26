namespace Domain.Interfaces
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<List<TModel>> Get();
        Task<TModel> Get(int id);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(TModel model);
        void CreateRange(IEnumerable<TModel> models);
        void UpdateRange(IEnumerable<TModel> models);
        void DeleteRange(IEnumerable<TModel> models);
    }
}
