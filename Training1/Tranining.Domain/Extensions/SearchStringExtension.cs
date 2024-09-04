using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;

namespace Tranining.Domain.Extensions
{
	public static class SearchStringExtension
	{
		public static IQueryable<T> SearchString<T>(this IQueryable<T> source, string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				return source;
			}

			var parameter = Expression.Parameter(typeof(T), "item");
			var properties = typeof(T).GetProperties();
			Expression orExpression = null;

			foreach (var property in properties)
			{
				if (property.PropertyType == typeof(string))
				{
					var propertyAccess = Expression.Property(parameter, property);
					var constantValue = Expression.Constant(searchString);
					var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					var containsExpression = Expression.Call(propertyAccess, containsMethod, constantValue);

					if (orExpression == null)
					{
						orExpression = containsExpression;
					}
					else
					{
						orExpression = Expression.OrElse(orExpression, containsExpression);
					}
				}
			}

			if (orExpression == null)
			{
				return source;
			}

			var lambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);
			return source.Where(lambda);
		}

	}
}
