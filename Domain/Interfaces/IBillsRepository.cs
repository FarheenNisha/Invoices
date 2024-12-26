using Domain.Models;

namespace Domain.Interfaces
{
    public interface IBillsRepository : IRepository<Bills>
    {
        Task<Bills> GetDuplicate(Bills model);
    }
}
