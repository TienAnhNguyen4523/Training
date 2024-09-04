using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Extensions
{
	public static class SearchingExtension
	{
		public static IQueryable<T> Search<T>(this IQueryable<T> source, List<SearchObjForCondition> searchConditions)
		{
			foreach (var condition in searchConditions)
			{
				PropertyInfo property = typeof(T).GetProperty(condition.Field);
				if (property != null)
				{
					var param = Expression.Parameter(typeof(T), "item");
					var propertyAccess = Expression.Property(param, property);
					var constantValue = Expression.Constant(condition.Value);
					var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					var body = Expression.Call(propertyAccess, containsMethod, constantValue);

					var predicate = Expression.Lambda<Func<T, bool>>(body, param);
					source = source.Where(predicate);
				}
			}
			return source;
		}
	}
}
