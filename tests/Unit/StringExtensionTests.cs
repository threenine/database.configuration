using Threenine;
using Xunit;

namespace Unit.Tests;

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