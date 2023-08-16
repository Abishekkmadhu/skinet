using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity //Place holder type t //specifing what T can be // the classes derived from baseentity // base entity is id which contained in all the entity 
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpecifications<T> spec);

        Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec);
    }
}
