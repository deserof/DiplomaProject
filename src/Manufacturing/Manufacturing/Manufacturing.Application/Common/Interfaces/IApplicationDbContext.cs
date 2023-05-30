using Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manufacturing.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Detail> Details { get; }

        DbSet<Employee> Employees { get; }

        DbSet<ProductionProcess> ProductionProcesses { get; }

        DbSet<Product> Products { get; }

        DbSet<ProductionLine> ProductionLines { get; }

        DbSet<ProductionOrder> ProductionOrders { get; }

        DbSet<ProcessExecution> ProcessExecutions { get; }

        DbSet<QualityControl> QualityControls { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
