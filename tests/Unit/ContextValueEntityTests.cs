using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Unit.Tests.TestFixtures;
using Xunit;

namespace Unit.Tests;

public class ContextValueEntityTests : IClassFixture<InMemoryFixture>
{
    private readonly InMemoryFixture _fixture;

    public ContextValueEntityTests(InMemoryFixture fixture)
    {
        _fixture = fixture;
        _fixture.Context.Database.EnsureCreated();
    }

    [Fact]
    public async Task ShouldSaveDateTime()
    {
        var repo = _fixture.Context.TestValueEntities;
        var save = await repo.AddAsync(new TestValueListEntity { Name = "test" });
  
        await  _fixture.Context.SaveChangesAsync();
      
       save.Entity.Id.ShouldBeOfType<int>();
    }
    
    [Fact]
    public async Task ShouldGetChangeTrackerEntries()
    {
        var repo = _fixture.Context.TestValueEntities;
        var save = await repo.AddAsync(new TestValueListEntity { Name = "test" });

        _fixture.Context.ChangeTracker.DetectChanges();
        var entries = _fixture.Context.ChangeTracker.Entries<TestEntity>().FirstOrDefault();
        await _fixture.Context.SaveChangesAsync();
        
        
       
        save.Entity.Id.ShouldBeOfType<int>();
    }
    
}