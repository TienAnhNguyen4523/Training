﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;

namespace Tranining.Domain.Extensions
{
	public static class FilterExtension
	{
		public static IQueryable<T> Filter<T>(this IQueryable<T> input, List<SearchObjForCondition> filter)
		{
			foreach (var item in filter) {
				PropertyInfo getter = typeof(T).GetProperty(item.Field);
				var param = Expression.Parameter(typeof(T));
				if(getter != null)
				{
					var body = Expression.Equal(
						Expression.Property(param, getter.Name),
						Expression.Constant(item.Value));
					MethodCallExpression result = Expression.Call(
						typeof(Queryable),
						"where",
						new[] { typeof(T) },
						input.Expression,
						Expression.Lambda<Func<T, bool>>(body, param));
					input = input.Provider.CreateQuery<T>(result);
				}
			}
			return input;
		}
	}
}
