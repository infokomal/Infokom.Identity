using Infokom.Identity.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infokom.Identity.Data
{
	internal class IdentityDataContext : IdentityDbContext<User, Role, int, User.Claim, Grant, User.Login, Role.Claim, User.Token>
	{

		public IdentityDataContext()
		{

		}

		public IdentityDataContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information)
					.EnableDetailedErrors();

				optionsBuilder.UseSqlServer("Server=(local);Database=Infokom;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=SSPI;");
			}
		}

		protected override void OnModelCreating(ModelBuilder m)
		{
			base.OnModelCreating(m);

			m.HasDefaultSchema("Identity");

			m.ApplyConfigurationsFromAssembly(typeof(IdentityDataContext).Assembly);
		}
	}
}
