using Infokom.Identity.App.Models;
using Infokom.Identity.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Infokom.Identity.App.Commands
{
	public record UserCreateCommandRequest : IRequest<UserCreateCommandResponse>
	{

		public record ContactInfo
		{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; } = "";

			public bool IsEmailConfirmed { get; set; }

			[Phone]
			[Display(Name = "Phone")]
			public string Phone { get; set; } = "";

			public bool IsPhoneConfirmed { get; set; }
		}

		

		public record PreferencesInfo
		{
			[Required]
			public CountryInfo Country { get; set; } = CountryInfo.GetByCode("ALB");
			
			[Required]
			public LanguageInfo Language { get; set; } = LanguageInfo.GetByCode("sqi");

			[Required]
			public CurrencyInfo Currency { get; set; } = CurrencyInfo.GetByCode("ALL");
		}


		public record AccountInfo
		{
			[Required]
			[StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
			[DataType(DataType.Text)]
			public string Username { get; set; } = "";

			[Required]
			[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
			[StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; } = "";

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; } = "";
		}

		public ContactInfo Contact { get; set; } = new ContactInfo();		

		public PreferencesInfo Preferences { get; set; } = new PreferencesInfo();

		public AccountInfo Account { get; set; } = new AccountInfo();



		public bool HasTwoFactor { get; set; }

		public bool IsLocked { get; set; }

		public string Prename { get; set; } = "";
		public string Surname { get; set; } = "";
		public DateOnly? BirthDate { get; set; }
		public CountryInfo BirthCountry { get; set; }

		


	}




	public class UserCreateCommandResponse
	{
		public User Data { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; }
	}

	

	public class CreateUserRequestHandler : IRequestHandler<UserCreateCommandRequest, UserCreateCommandResponse>
	{
		private readonly UserManager<User> _userManager;

		public CreateUserRequestHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<UserCreateCommandResponse> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
		{
			var user = new User()
			{
				UserName = request.Account.Username,
				Email = request.Contact.Email,
				PhoneNumber = request.Contact.Phone
			};


			var result = request.Account.Password != request.Account.ConfirmPassword ? IdentityResult.Failed(_userManager.ErrorDescriber.PasswordMismatch()) : IdentityResult.Success;


			if (result.Succeeded)
			{
				

				result = await _userManager.CreateAsync(user, request.Account.Password).WaitAsync(cancellationToken);


				if (result.Succeeded)
				{
					var claims = new List<Claim>()
					{
						new ("Country", request.Preferences.Country.Code),
						new ("Language", request.Preferences.Language.Code),
						new ("Currency", request.Preferences.Currency.Code)
					};

					result = await _userManager.AddClaimsAsync(user, claims).WaitAsync(cancellationToken);
				}
			}

			return new UserCreateCommandResponse()
			{
				Data = user,
				Success = result.Succeeded,
				Message = result.ToString()
			};
		}
	}
}
