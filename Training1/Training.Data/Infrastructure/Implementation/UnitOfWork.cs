using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;

namespace Training.Data.Infrastructure.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		#region Private Fields
		private readonly DbContext _dbContext;
		
		public IDbConnection Connection { get; }
		public virtual IRepository<User> User { get; }
		public virtual IRepository<Role> Role { get; }
		public virtual IRepository<UserDetail> UserDetail { get; }
		public virtual IRepository<UserRole> UserRole { get; }
		public virtual IRepository<Author> Author { get; }
		public virtual IRepository<Book> Book { get; }
		public virtual IRepository<Category> Category { get; }
		public virtual IRepository<ProductRole> ProductRole { get; }
		public virtual IRepository<Permission> Permission { get; }
		public virtual IRepository<UserPermission> UserPermission { get; }
		public virtual IRepository<BorrowAndReturnBook> BorrowAndReturnBook { get; }
		public virtual IRepository<ExportBill> ExportBill { get; }

		#endregion

		#region Constructors
		public UnitOfWork(DbContext dbContext,
			IRepository<User> user,
			IRepository<Role> role,
			IRepository<UserDetail> userDetail,
			IRepository<UserRole> userRole,
			IRepository<Author> author,
			IRepository<Book> book,
			IRepository<Category> category,
			IRepository<ProductRole> productRole,
			IRepository<Permission> permission,
			IRepository<UserPermission> userPermission,
			IRepository<BorrowAndReturnBook> borrowAndReturnBook,
			IRepository<ExportBill> exportBill

			)

		{
			_dbContext = dbContext;

			User = user;
			Role = role;
			UserDetail = userDetail;
			UserRole = userRole;
			Author = author;
			Book = book;
			Category = category;
			ProductRole = productRole;
			Permission = permission;
			UserPermission = userPermission;
			BorrowAndReturnBook = borrowAndReturnBook;
			ExportBill = exportBill;
		}
		#endregion

		#region Methods
		public IDbContextTransaction BeginTransactionScope()
		{
			return _dbContext.Database.BeginTransaction();
		}

		public int Commit()
		{
			return _dbContext.SaveChanges();
		}

		public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.SaveChangesAsync(cancellationToken);
		}
		#endregion
	}
}


