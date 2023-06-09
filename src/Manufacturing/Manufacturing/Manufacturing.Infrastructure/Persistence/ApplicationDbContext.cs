using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using Manufacturing.Infrastructure.Common;
using Manufacturing.Infrastructure.Identity;
using Manufacturing.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Manufacturing.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(
            DbContextOptions options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
            IMediator mediator)
            : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            _mediator = mediator;
        }
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<ProductionProcess> ProductionProcesses => Set<ProductionProcess>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductionOrder> ProductionOrders => Set<ProductionOrder>();
        public DbSet<ProcessExecution> ProcessExecutions => Set<ProcessExecution>();
        public DbSet<QualityControl> QualityControls => Set<QualityControl>();
        public DbSet<ProductFile> ProductFiles => Set<ProductFile>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            builder.Entity<ProcessExecution>()
                .HasOne(pe => pe.ProcessFile)
                .WithMany()
                .HasForeignKey(pe => pe.ProcessFileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProcessExecution>()
                .HasOne(pe => pe.ProcessPhoto)
                .WithMany()
                .HasForeignKey(pe => pe.ProcessPhotoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
