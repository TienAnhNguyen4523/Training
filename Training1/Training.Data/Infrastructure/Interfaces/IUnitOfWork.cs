using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Entity.EntityModel;
using Training.Data.DBContext;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Training.Data.Infrastructure.Interfaces
{
	public interface IUnitOfWork
	{
		IDbConnection Connection { get; }
		
		#region Repositories
		IRepository<User> User { get; }
		IRepository<Role> Role { get; }
		IRepository<UserDetail> UserDetail { get; }
		IRepository<UserRole> UserRole { get; }
		IRepository<Author> Author { get; }
		IRepository<Book> Book { get; }

		IRepository<Category> Category { get; }
		IRepository<ProductRole> ProductRole { get; }
		IRepository<Permission> Permission { get; }
		IRepository<UserPermission> UserPermission { get; }
		IRepository<BorrowAndReturnBook> BorrowAndReturnBook { get; }
		IRepository<ExportBill> ExportBill { get; }
		#endregion
		#region Methods
		int Commit();
		Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
		IDbContextTransaction BeginTransactionScope();
		#endregion

	}
}
