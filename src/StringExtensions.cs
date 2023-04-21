using System.Text;

namespace Threenine;

public static class StringExtensions
{
  public static string ToSnakeCase(this string text)
  {
    var sb = new StringBuilder();
    sb.Append(char.ToLowerInvariant(text[0]));
    for(var i = 1; i < text.Length; ++i) {
      var c = text[i];
      if(char.IsUpper(c)) {
        sb.Append('_');
        sb.Append(char.ToLowerInvariant(c));
      } else {
        sb.Append(c);
      }
    }
    return sb.ToString();
  }
}