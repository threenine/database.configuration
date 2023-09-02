using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Threenine.Models;

namespace Threenine;

public abstract class BaseContext<T> : DbContext where T : DbContext
{
    protected BaseContext(DbContextOptions<T> options) : base(options)
    {
      
        base.ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }
    public override int SaveChanges()
    {
    
       AuditDates(ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Detached)
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
                if (entry.Entity is BaseEntity)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                    ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
                }

                break;
            case EntityState.Modified:
                if (entry.Entity is BaseEntity)
                {
                    ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
                }
               
                break;
            case EntityState.Detached:
                if (entry.Entity is BaseEntity)
                {
                    ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
                }
                
                break;
            case EntityState.Unchanged:
            case EntityState.Deleted:
            default:
                if (entry.Entity is BaseEntity)
                {
                    ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
                }
               
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