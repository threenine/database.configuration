using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Unit.Tests.TestFixtures;
using Xunit;

namespace Unit.Tests;

public class ContextBaseEntityTests : IClassFixture<InMemoryFixture>
{
    private readonly InMemoryFixture _fixture;

    public ContextBaseEntityTests(InMemoryFixture fixture)
    {
        _fixture = fixture;
        _fixture.Context.Database.EnsureCreated();
    }

    [Fact]
    public async Task ShouldSaveDateTime()
    {
        var repo = _fixture.Context.TestEntities;
        var save = await repo.AddAsync(new TestEntity { Name = "test" });
  
        await  _fixture.Context.SaveChangesAsync();
        
       save.Entity.Created.ShouldBeOfType<DateTime>();
       save.Entity.Modified.ShouldBeOfType<DateTime>();
       save.Entity.Id.ShouldBeOfType<Guid>();
    }
    
    [Fact]
    public async Task ShouldGetChangeTrackerEntries()
    {
        var repo = _fixture.Context.TestEntities;
        var save = await repo.AddAsync(new TestEntity { Name = "test" });

        _fixture.Context.ChangeTracker.DetectChanges();
        var entries = _fixture.Context.ChangeTracker.Entries<TestEntity>().FirstOrDefault();
        await _fixture.Context.SaveChangesAsync();
        
        save.Entity.Created.ShouldBeOfType<DateTime>();
        save.Entity.Modified.ShouldBeOfType<DateTime>();
        save.Entity.Id.ShouldBeOfType<Guid>();
    }
    
}