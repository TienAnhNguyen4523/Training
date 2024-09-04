using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Extensions
{
	public static class PagingExtension
	{
		public static async Task<PaginationSet<T>> Pagination<T>(this IQueryable<T> source, int pageIndex, int pageSize, SearchObjForCondition sortExpressions)
		{
			var totalCount = source.Count();
			var param = Expression.Parameter(typeof(T), "item");
			var sortExpression = Expression.Lambda<Func<T, object>>
				(Expression.Convert(Expression.Property(param,sortExpressions.Field),typeof(object)), param);
			if(sortExpressions == null || sortExpressions.Value != "asc")
			{
				source = source.OrderByDescending(sortExpression);
			}
			else
			{
				source = source.OrderBy(sortExpression);
			}
			var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
			var result = new PaginationSet<T>
			{
				PageIndex = pageIndex,
				Count = items?.Count() ?? 0,
				TotalCount = totalCount,
				ToltalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
				Items = items
			};
			return result;

		}
	}
}
