using Infokom.Identity.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infokom.Identity.App.Commands
{
    public class UserLockToggleRequest : IRequest<IdentityResult>
	{
		public string User { get; set; }
		public bool IsLocked { get; set; }
	}

	public class UserLockToggleRequestHandler : IRequestHandler<UserLockToggleRequest, IdentityResult>
	{
		private readonly UserManager<User> _userManager;
		public UserLockToggleRequestHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IdentityResult> Handle(UserLockToggleRequest request, CancellationToken cancellationToken)
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

			return await _userManager.SetLockoutEnabledAsync(user, request.IsLocked).WaitAsync(cancellationToken);
		}
	}
}
