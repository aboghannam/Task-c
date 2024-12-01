using Application.Abstractions.Data;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{

    public DbSet<UserData> UserDatas { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Domain.Entities.Setting> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.Entity<UserData>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Appointment>().HasQueryFilter(p => !p.IsDeleted);

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedTime = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedTime = DateTime.UtcNow;
            }
        }

        int result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

}
