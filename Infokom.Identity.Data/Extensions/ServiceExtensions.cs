using Infokom.Identity.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infokom.Identity.Data.Extensions
{
	public static class ServiceExtensions
	{

		public static IServiceCollection AddInfokomIdentityData(this IServiceCollection services)
		{
			services.AddDbContext<IdentityDataContext>(optionsLifetime: ServiceLifetime.Transient);

			services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<IdentityDataContext>();



			return services;
		}
	}
}
