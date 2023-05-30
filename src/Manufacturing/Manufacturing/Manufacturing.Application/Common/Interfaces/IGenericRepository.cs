using Manufacturing.Application.Common.Models;
using Manufacturing.Domain.Common;

namespace Manufacturing.Application.Common.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task DeleteAsync(T entity, CancellationToken cancellationToken);

        Task<PaginatedList<TResult>> GetAsync<TSource, TResult>(int pageNumber, int pageSize,
            CancellationToken cancellationToken)
            where TSource : class
            where TResult : class;

        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
