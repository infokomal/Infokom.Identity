using Infokom.Identity.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infokom.Identity.Data
{
    internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>, IEntityTypeConfiguration<Role.Claim>
	{
		public void Configure(EntityTypeBuilder<Role> e)
		{
			e.ToTable("Role");

			e.HasData([
				new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = null },
				new Role { Id = 2, Name = "Officer", NormalizedName = "OFFICER", ConcurrencyStamp = null },
				new Role { Id = 3, Name = "Operator", NormalizedName = "OPERATOR", ConcurrencyStamp = null },
				new Role { Id = 4, Name = "Applicant", NormalizedName = "APPLICANT", ConcurrencyStamp = null },
				new Role { Id = 5, Name = "Guest", NormalizedName = "GUEST", ConcurrencyStamp = null },
			]);
		}

		public void Configure(EntityTypeBuilder<Role.Claim> e)
		{
			e.ToTable("Role.Claim");
		}
	}
}
