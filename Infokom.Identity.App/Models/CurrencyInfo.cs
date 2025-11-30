using System.Globalization;
using System.Text.RegularExpressions;

namespace Infokom.Identity.App.Models
{
    public sealed record CurrencyInfo
	{
		private static readonly Dictionary<string, string> DATA = CultureInfo
			.GetCultures(CultureTypes.SpecificCultures)
			.Select(c => new RegionInfo(c.Name))
			.Where(c => Regex.IsMatch(c.ISOCurrencySymbol, "^[A-Z]{3}$"))
			.Select(c => KeyValuePair.Create(c.ISOCurrencySymbol, c.CurrencyEnglishName))
			.DistinctBy(x => x.Key)
			.ToDictionary();

		private CurrencyInfo(string code, string name)
		{
			this.Code = code;
			this.Name = name;
		}

		public string Code { get; }

		public string Name { get; }


		public static CurrencyInfo GetByCode(string code)
		{
			if (DATA.TryGetValue(code, out var name))
			{
				return new CurrencyInfo(code, name);
			}

			return null;
		}

		public static IEnumerable<CurrencyInfo> Currencies => DATA.OrderBy(x => x.Value).Select(kv => new CurrencyInfo(kv.Key, kv.Value));
	}
}
