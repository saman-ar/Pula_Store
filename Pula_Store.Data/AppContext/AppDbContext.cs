using Entities.AppIdentities;
using Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Identity.Stores;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AppContext
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductFeatures> Features { get; set; }
		public DbSet<ProductImages> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AppUser>()
                                    .HasQueryFilter(f => !f.IsRemoved);
			builder.Entity<Category>()
                                    .HasQueryFilter(c => !c.IsRemoved);
            builder.Entity<Product>()
                                    .HasQueryFilter(p => !p.IsRemoved).HasQueryFilter(p=>p.Displayed);

            builder.Entity<Product>()
                                    .HasOne(p => p.Category)
                                    .WithMany(p=>p.Products)
                                    .HasForeignKey(p=>p.CategoryId)
                                    .IsRequired(false)
                                    .OnDelete(DeleteBehavior.SetNull);

			builder.Entity<Product>()
                                    .HasMany(p => p.Images)
                                    .WithOne(p => p.Product)
                                    .HasForeignKey(p => p.ProductId)
                                    .IsRequired()
                                    .OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Product>()
                                    .HasMany(p => p.Features)
                                    .WithOne(p => p.Product)
                                    .HasForeignKey(p => p.ProductId)
                                    .IsRequired()
                                    .OnDelete(DeleteBehavior.Cascade);


			//builder.Entity<Category>().Property(nameof(Category.Name)).HasComputedColumnSql("");
			//.IsUnicode(false);
			//builder.Entity<Category>().HasOne(c => c.SubCategories).WithMany().IsRequired().OnDelete(DeleteBehavior.Cascade);

			//builder.HasSequence<int>("OrderNumber", "SchemaName").StartsAt(100).IncrementsBy(2);
		}
	}

	//public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext<AppUser>>
	//{
	//	public AppDbContext<AppUser> CreateDbContext(string[] args)
	//	{
	//		var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext<AppUser>>();

	//		var connectionString = "Data Source=(localdb)\\v11.0;Initial Catalog=Pula_Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

	//		dbContextBuilder.UseSqlServer(connectionString/*,x=>x.MigrationsAssembly("Data.Migrations")*/);

	//		return new AppDbContext<AppUser>(dbContextBuilder.Options);
	//	}
	//}
}
