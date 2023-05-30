using AutoMapper;
using AutoMapper.QueryableExtensions;
using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Common.Mappings;
using Manufacturing.Application.Common.Models;
using Manufacturing.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Manufacturing.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenericRepository(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TResult>> GetAsync<TSource, TResult>(int pageNumber, int pageSize, CancellationToken cancellationToken)
            where TSource : class
            where TResult : class
        {
            return await _dbContext.Set<TSource>()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(pageNumber, pageSize);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
