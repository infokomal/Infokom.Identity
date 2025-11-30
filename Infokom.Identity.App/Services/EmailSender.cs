using Infokom.Identity.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infokom.Identity.App.Services
{
	internal abstract class EmailSender : IEmailSender<User>
	{
		public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
		{
			var subject = "Confirm your email";

			var body = $"Dear {user.UserName},<br>please confirm your account by clicking this <a href='{confirmationLink}'>link</a>.";

			await this.SendAsync(email, subject, body);
		}

		public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
		{ 			
			var subject = "Reset your password";

			var body = $"Dear {user.UserName},<br>use the code <strong>{resetCode}</strong> to reset your password.";

			await this.SendAsync(email, subject, body);
		}

		public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
		{
			var subject = "Reset your password";

			var body = $"Dear {user.UserName},<br>please reset your password by clicking this <a href='{resetLink}'>link</a>.";

			await this.SendAsync(email, subject, body);
		}

		public async Task SendConfirmationLinkAsync(User user, string confirmationLink) => await this.SendConfirmationLinkAsync(user, user.Email, confirmationLink);

		public async Task SendPasswordResetCodeAsync(User user, string resetCode) => await this.SendPasswordResetCodeAsync(user, user.Email, resetCode);

		public async Task SendPasswordResetLinkAsync(User user, string resetLink) => await this.SendPasswordResetLinkAsync(user, user.Email, resetLink);




		protected abstract Task SendAsync(string email, string subject, string body);
        
    }
}
