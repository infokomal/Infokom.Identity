using Infokom.Identity.App.Services;
using Infokom.Identity.Core;
using Infokom.Identity.Data;
using Infokom.Identity.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infokom.Identity.App.Extensions
{
    public static class ServiceExtensions
    {

		public static IServiceCollection AddInfokomIdentity(this IServiceCollection services)
		{
			services.AddInfokomIdentityData();			

			services.AddScoped<IEmailSender<User>, SmtpEmailSender>();

			services.AddMediatR(services => services.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			return services;
		}
    }
}
