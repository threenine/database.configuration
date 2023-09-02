using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Unit.Tests.TestFixtures;

public class InMemoryFixture : IDisposable
{
    public TestDbContext Context => InMemoryContext();


    private static TestDbContext InMemoryContext()
    {
      
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite("DataSource=:memory:")
             .Options;

        var context = new TestDbContext(options);
        context.ChangeTracker.AutoDetectChangesEnabled = true;
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        context.TestEntities.Add(new TestEntity { Name = "Test Entity" });
        context.TestValueEntities.Add(new TestValueListEntity { Name = "Test Value Entity" });
        context.SaveChanges();
        return context;
    }
    public void Dispose()
    {
        Context?.Dispose();
    }
}