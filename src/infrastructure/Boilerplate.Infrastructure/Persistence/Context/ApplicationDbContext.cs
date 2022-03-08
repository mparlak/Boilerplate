using Boilerplate.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Boilerplate.Infrastructure.Persistence.Context;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = Guid.NewGuid();
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = Guid.NewGuid();
                    break;

                // case EntityState.Deleted:
                //     if (entry.Entity is ISoftDelete softDelete)
                //     {
                //         softDelete.DeletedBy = userId;
                //         softDelete.DeletedOn = DateTime.UtcNow;
                //         entry.State = EntityState.Modified;
                //     }
                //     break;
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = Guid.NewGuid();
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = Guid.NewGuid();
                    break;

                // case EntityState.Deleted:
                //     if (entry.Entity is ISoftDelete softDelete)
                //     {
                //         softDelete.DeletedBy = userId;
                //         softDelete.DeletedOn = DateTime.UtcNow;
                //         entry.State = EntityState.Modified;
                //     }
                //     break;
            }
        }

        //ChangeTracker.DetectChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }
}