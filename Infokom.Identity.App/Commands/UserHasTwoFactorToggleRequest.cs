using Infokom.Identity.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infokom.Identity.App.Commands
{
	public class UserHasTwoFactorToggleRequest : IRequest<IdentityResult>
	{
		public string User { get; set; }
		public bool Enabled { get; set; }
	}

	public class UserHasTwoFactorToggleRequestHandler : IRequestHandler<UserHasTwoFactorToggleRequest, IdentityResult>
	{
		private readonly UserManager<User> _userManager;
		public UserHasTwoFactorToggleRequestHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IdentityResult> Handle(UserHasTwoFactorToggleRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByNameAsync(request.User).WaitAsync(cancellationToken);
			if (user == null)
			{
				return IdentityResult.Failed(new IdentityError()
				{
					Code = "UserNotFound",
					Description = $"User '{request.User}' not found."
				});
			}
			return await _userManager.SetTwoFactorEnabledAsync(user, request.Enabled).WaitAsync(cancellationToken);
		}
	}
}
