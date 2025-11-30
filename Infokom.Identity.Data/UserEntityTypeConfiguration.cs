using Infokom.Identity.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infokom.Identity.Data
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<User.Claim>, IEntityTypeConfiguration<User.Login>, IEntityTypeConfiguration<User.Token>
	{
		public void Configure(EntityTypeBuilder<User> e)
		{
			e.ToTable("User");

			e.HasData([
				new User { Id = 1, UserName = "admin", NormalizedUserName = "ADMIN", ConcurrencyStamp = null }
			]);
		}

        public void Configure(EntityTypeBuilder<User.Claim> e)
		{
			e.ToTable("User.Claim");
		}
		public void Configure(EntityTypeBuilder<User.Login> e)
		{
			e.ToTable("User.Login");
		}
		public void Configure(EntityTypeBuilder<User.Token> e)
		{
			e.ToTable("User.Token");
		}
	}
}
