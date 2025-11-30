using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Infokom.Identity.App.Models
{
	public class UserInfo
	{
		public record EmailInfo
		{
			public string Address { get; set; }

			public bool IsConfirmed { get; set; }
		}

		public record PhoneInfo
		{
			public string Number { get; set; }

			public bool IsConfirmed { get; set; }
		}


		[Display(Name = "User Name")]
		public string Name { get; set; } = "";

		[EmailAddress]
		[Display(Name = "Email")]
		public EmailInfo Email { get; set; } = new();

		[Phone]
		[Display(Name = "Phone")]
		public PhoneInfo Phone { get; set; } = new();

		public bool Has2FA { get; set; }


		public bool IsLocked { get; set; }

		public RegionInfo Country { get; set; } = new RegionInfo(CultureInfo.CurrentCulture.Name);

		public CultureInfo Language { get; set; } = CultureInfo.CurrentCulture;

	}


	public record Item(string Key, string Value);


}
