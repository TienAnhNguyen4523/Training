using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Core;
using Training.Entity.EntityModel;

namespace Training.Data.Infrastructure.Interfaces
{
	public interface IRepository<T>
	{
		#region Methods
		void Insert(T entity);
		void Remove(IEnumerable<T> entities);
		void Remove(T entity);
		void Insert(IEnumerable<T> lstEntity);
		void Update(T entityToUpdate);
		void Update(IEnumerable<T> entityToUpdate);
		Task<bool> CheckExist(Expression<Func<T, bool>> predicate, Ref<CheckError> checkError = null);
		Task<int> GetCount(Expression<Func<T, bool>> predicare, Ref<CheckError> checkError = null);
		Task<T> GetById(Object id, Ref<CheckError> checkError = null);
		Task<IEnumerable<T>> Get(string storedProcedureName, SqlParameter[] paramaters = null, Ref<CheckError> checkError = null);
		Task<T> GetOne(string storedProcedureName, SqlParameter[] paramaters = null, Ref<CheckError> checkError = null);
		IEnumerable<SqlParameter> GetOutPut(string storedProcedureName, SqlParameter[] parameters, Ref<CheckError> checkError = null);
		IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
		Task<bool> Delete(object id, Ref<CheckError> checkError = null);
		Task<bool> Delete(T entity, Ref<CheckError> checkError = null);
		Task<bool> DeleteAll(IList<T> list, Ref<CheckError> checkError = null);
		IQueryable<T> GetQueryable();
		IQueryable<T> GetQueryable(Expression<Func<T, bool>> condition);
		Task<T> GetOne(Expression<Func<T, bool>> predicate, Ref<CheckError> checkError = null);
	
		#endregion
	}
}
