using System.Text.RegularExpressions;

namespace JdmDepot.UI.Util;

public static class TextHelper
{
	private static readonly Regex KebabCase = new(@"(\p{Lu}+)(\p{Lu}[^\p{Lu}]|$)", RegexOptions.Compiled);

	public static string ToKebabCase(string input) =>
		KebabCase.Replace(input, "$1-$2");
}
