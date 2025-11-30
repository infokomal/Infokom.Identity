namespace Infokom.Identity.App.Services
{
    internal class SmtpEmailSender : EmailSender
	{
		protected override async Task SendAsync(string email, string subject, string body)
		{
			using (var smtpClient = new System.Net.Mail.SmtpClient())
			{
				using (var mailMessage = new System.Net.Mail.MailMessage("identity@infokom.al", email, subject, body))
				{
					await smtpClient.SendMailAsync(mailMessage);
				}
			}
		}
	}
}
