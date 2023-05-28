using Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manufacturing.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Detail> Details { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
