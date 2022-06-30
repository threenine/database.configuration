using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Threenine.Models;

namespace Threenine;

public abstract class BaseContext<T> : DbContext where T : DbContext
{
    protected BaseContext(DbContextOptions<T> options) : base(options)
    {
      
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }
    public override int SaveChanges()
    {
    
       AuditDates(ChangeTracker.Entries()
            .Where(E => E.State is EntityState.Added or EntityState.Modified or EntityState.Detached)
            .ToList());
        
        return base.SaveChanges();
    }
    private static void AuditDates(IEnumerable<EntityEntry> entries)
    {
        foreach (var entry in entries)
        {
           UpdateEntity(entry);
        }
    }
 
    private static void ChangeTracker_StateChanged(object sender, EntityStateChangedEventArgs e)
    {
        UpdateEntity(e.Entry);
       
    }

    
    internal static void UpdateEntity(EntityEntry entry)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                ((BaseEntity)entry.Entity).Created = DateTime.Now;
                ((BaseEntity)entry.Entity).Modified = DateTime.Now;
                break;
            case EntityState.Modified:
                ((BaseEntity)entry.Entity).Modified = DateTime.Now;
                break;
            case EntityState.Detached:
                ((BaseEntity)entry.Entity).Modified = DateTime.Now;
                break;
            case EntityState.Unchanged:
            case EntityState.Deleted:
            default:
                ((BaseEntity)entry.Entity).Modified = DateTime.Now;
                break;
        }
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        AuditDates(ChangeTracker.Entries());
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        AuditDates(ChangeTracker.Entries());
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AuditDates(ChangeTracker.Entries());
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
}