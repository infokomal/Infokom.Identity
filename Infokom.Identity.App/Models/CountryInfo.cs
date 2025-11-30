using System.Globalization;
using System.Text.RegularExpressions;

namespace Infokom.Identity.App.Models
{
    public sealed record CountryInfo
	{
		private static readonly Dictionary<string, string> DATA = CultureInfo
			.GetCultures(CultureTypes.SpecificCultures)
			.Select(c => new RegionInfo(c.Name))
			.Where(c => Regex.IsMatch(c.ThreeLetterISORegionName, "^[A-Z]{3}$"))
			.Select(c => KeyValuePair.Create(c.ThreeLetterISORegionName, c.EnglishName))
			.DistinctBy(x => x.Key)
			.ToDictionary();

		private CountryInfo(string code, string name) 
		{
			this.Code = code;
			this.Name = name;
		}

		public string Code { get; }

		public string Name { get; }


		public static CountryInfo GetByCode(string code)
		{
			if (DATA.TryGetValue(code, out var name))
			{
				return new CountryInfo(code, name);
			}

			return null;
		}

		public static IEnumerable<CountryInfo> Countries => DATA.OrderBy(x => x.Value).Select(kv => new CountryInfo(kv.Key, kv.Value));
	}
}
