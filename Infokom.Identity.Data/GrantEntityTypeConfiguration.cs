using Infokom.Identity.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infokom.Identity.Data
{
    internal class GrantEntityTypeConfiguration : IEntityTypeConfiguration<Grant>
	{
		public void Configure(EntityTypeBuilder<Grant> e)
		{
			e.ToTable("Grant");
		}
	}
}
