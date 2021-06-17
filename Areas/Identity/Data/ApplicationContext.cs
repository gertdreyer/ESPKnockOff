using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ESPKnockOff.Models;

namespace ESPKnockOff.Data {
	public class ApplicationContext : IdentityDbContext<IdentityUser> {
		public DbSet<TimeCode> TimeCode { get; set; }
		public DbSet<LoadSheddingSlot> LoadSheddingSlot { get; set; }
		public DbSet<SuburbCluster> SuburbCluster { get; set; }
		public DbSet<Suburb> Suburb { get; set; }
		public DbSet<Municipality> Municipality { get; set; }
		public DbSet<Province> Province { get; set; }
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options) {
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}
	}
}
