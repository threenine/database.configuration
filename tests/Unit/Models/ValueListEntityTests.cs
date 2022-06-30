using System.ComponentModel;
using Shouldly;
using Threenine.Models;
using Xunit;

namespace Unit.Tests.Models;

public class ValueListEntityTests
{
    [Theory, Description("Ensure the Base Entity Class has the following fields defined")]
    [InlineData("Id", typeof(Guid))]
    [InlineData("Name", typeof(string))]
    [InlineData("Description", typeof(string))]
    [InlineData("Created", typeof(DateTime))]
    [InlineData("Modified", typeof(DateTime))]
    [InlineData("Active", typeof(bool?))]
    public void Should_have_created_field_defined(string name, Type type)
    {
        var testClass = typeof(ValueListEntity);
        var prop = testClass.GetProperty(name);

        prop.ShouldSatisfyAllConditions(
            () => prop.ShouldNotBeNull(),
            () => prop?.PropertyType.ShouldBeEquivalentTo(type)
        );
    }
}