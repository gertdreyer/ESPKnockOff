using System;
using ESPKnockOff.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ESPKnockOff.Areas.Identity.IdentityHostingStartup))]
namespace ESPKnockOff.Areas.Identity {
	public class IdentityHostingStartup : IHostingStartup {
		public void Configure(IWebHostBuilder builder) {
			builder.ConfigureServices((context, services) => {
				services.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(
						Environment.GetEnvironmentVariable("SQLAZURECONNSTR_ESP")));

				services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
					.AddEntityFrameworkStores<ApplicationContext>();
			});
		}
	}
}