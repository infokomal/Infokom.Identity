using Infokom.Identity.App.Extensions;
using Infokom.Identity.Apps.Web.Backoffice.Components;
using Infokom.Identity.Core;
using Infokom.Identity.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;

namespace Infokom.Identity.Apps.Web.Backoffice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddFluentUIComponents();
			builder.Services.AddScoped<ITooltipService, TooltipService>();
               builder.Services.AddScoped<IDialogService, DialogService>();
               builder.Services.AddScoped<IToastService, ToastService>();

			builder.Services.AddCascadingAuthenticationState();


			builder.Services.AddInfokomIdentity();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<Components.App>()
                .AddInteractiveServerRenderMode();


            app.Run();
        }
    }
}
