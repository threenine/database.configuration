using System.ComponentModel;
using Shouldly;
using Threenine.Models;
using Xunit;

namespace Unit.Tests.Models;

public class BaseMappingEntityTests
{
    [Theory, Description("Ensure the Base Entity Class has the following fields defined")]
    [InlineData("Created", typeof(DateTime))]
    public void Should_have_created_field_defined(string name, Type type)
    {
        var testClass = typeof(BaseEntity);
        var prop = testClass.GetProperty(name);

        prop.ShouldSatisfyAllConditions(
            () => prop.ShouldNotBeNull(),
            () => prop?.PropertyType.ShouldBeEquivalentTo(type)
        );
    }
}