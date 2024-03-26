using Threenine.Database.Extensions;
using Xunit;

namespace Unit.Tests.Extensions;

public class StringExtensionTests
{
    [Theory]
    [InlineData("TestThis", "test_this")]
    [InlineData("ProfileId", "profile_id")]
    [InlineData("profileId", "profile_id")]
    public void ShouldReturnSnakeCase(string testString, string expected)
  {
    var result = testString.ToSnakeCase();
    Assert.Equal(expected, result);
  }
}