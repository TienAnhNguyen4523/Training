using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Entity.EntityModel;

namespace Training.Data.DBContext
{
	public class TrainingDbContext : DbContext
	{
		public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options) { }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<UserDetail> UserDetails { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<UserRole> UserRoles { get; set; }
		public virtual DbSet<Book> Books { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<ProductRole> ProductRoles { get; set; }
		public virtual DbSet<Category> CategoryRoles { get; set; }
		public virtual DbSet<Permission> Permissions { get; set; }
		public virtual DbSet<UserPermission> UserPermissions { get; set; }
		public virtual DbSet<BorrowAndReturnBook> BorrowAndReturnBooks { get; set; }
		public virtual DbSet<ExportBill> ExportBills { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder) { 

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

	}
}
