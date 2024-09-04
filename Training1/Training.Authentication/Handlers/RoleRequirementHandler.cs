using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Training.Authentication.ActionFilters;
using Training.Authentication.Requirements;

namespace Training.Authentication.Handlers
{
	public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
	{
		#region Properties
		private readonly IHttpContextAccessor _httpContextAccessor;
		#endregion

		#region Constructor
		public RoleRequirementHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		#endregion

		#region Methods
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
		{
			// Convert authorization filter context into authorization filter context.
			var authorizationFilterContext = (AuthorizationFilterContext)context.Resource;

			// Check context solidity.
			if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
			{
				context.Fail();
				return Task.CompletedTask;
			}

			// Find HttpContext.
			var httpContext = _httpContextAccessor.HttpContext;

			// Find account which has been embeded into HttpContext.
			if (!httpContext.Items.ContainsKey(ClaimTypes.Actor))
			{
				// Controller or method allow by pass information analyze.
				if (IsAbleToByPass(authorizationFilterContext))
				{
					context.Succeed(requirement);
					return Task.CompletedTask;
				}

				context.Fail();
				return Task.CompletedTask;
			}

			//context.Succeed(requirement);
			return Task.CompletedTask;
		}

		private bool IsAbleToByPass(AuthorizationFilterContext authorizationFilterContext)
		{
			return authorizationFilterContext.Filters.Any(x => x is ByPassAuthorizationAttribute);
		}
		#endregion
	}
}
