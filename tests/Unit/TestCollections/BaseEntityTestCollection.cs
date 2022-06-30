using Unit.Tests.TestFixtures;
using Xunit;

namespace Unit.Tests.TestCollections;

[CollectionDefinition(GlobalTestStrings.BaseEntityCollection)]
public class BaseEntityTestCollection :ICollectionFixture<InMemoryFixture>
{
    
}