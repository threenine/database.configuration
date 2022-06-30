using Microsoft.EntityFrameworkCore;
using Threenine;

namespace Unit.Tests;

public class TestDbContext : BaseContext<TestDbContext>
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<TestEntity> TestEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEntity>(entity =>
        {
            entity.ToTable(nameof(TestEntity));

            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Id);
            
            entity.Property(e => e.Name);
            entity.Property(e => e.Id);
            entity.Property(e => e.Created).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            entity.Property(e => e.Modified).ValueGeneratedOnUpdate().HasDefaultValueSql("getdate()");
        });
    }
}