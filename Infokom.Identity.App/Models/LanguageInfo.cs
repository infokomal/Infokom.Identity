using System.Globalization;
using System.Text.RegularExpressions;

namespace Infokom.Identity.App.Models
{
    public sealed record LanguageInfo
	{
		private static readonly Dictionary<string, string> DATA = CultureInfo
			.GetCultures(CultureTypes.NeutralCultures)
			.Where(c => Regex.IsMatch(c.ThreeLetterISOLanguageName, "^[a-z]{3}$"))
			.Select(c => KeyValuePair.Create(c.ThreeLetterISOLanguageName, c.EnglishName))
			.DistinctBy(x => x.Key)
			.ToDictionary();

		private LanguageInfo(string code, string name)
		{
			this.Code = code;
			this.Name = name;
		}

		public string Code { get; }

		public string Name { get; }


		public static LanguageInfo GetByCode(string code)
		{
			if (DATA.TryGetValue(code, out var name))
			{
				return new LanguageInfo(code, name);
			}

			return null;
		}

		public static IEnumerable<LanguageInfo> Languages => DATA.OrderBy(x => x.Value).Select(kv => new LanguageInfo(kv.Key, kv.Value));
	}
}
