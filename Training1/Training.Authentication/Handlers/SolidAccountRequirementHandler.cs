using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Training.Authentication.ActionFilters;
using Training.Authentication.Requirements;

namespace Training.Authentication.Handlers
{
	public class SolidAccountRequirementHandler : AuthorizationHandler<SolidAccountRequirement>
	{
		#region Properties
		private readonly IProfileService _identityService;

		private readonly IHttpContextAccessor _httpContextAccessor;
		#endregion
		#region Constructor
		public SolidAccountRequirementHandler(IProfileService identityService, IHttpContextAccessor httpContextAccessor)
		{
			_identityService = identityService;
			_httpContextAccessor = httpContextAccessor;
		}
		#endregion

		#region Methods
		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
			SolidAccountRequirement requirement)
		{
			// Convert authorization filter context into authorization filter context.
			var authorizationFilterContext = (AuthorizationFilterContext)context.Resource;

			//var httpContext = authorizationFilterContext.HttpContext;
			var httpContext = _httpContextAccessor.HttpContext;

			// Find claim identity attached to principal.
			var claimIdentity = (ClaimsIdentity)httpContext.User.Identity;

			// Find id from claims list.
			var id = claimIdentity.Claims.Where(x => x.Type.Equals("Id"))
				.Select(x => x.Value)
				.FirstOrDefault();


			// Id is invalid
			//if (string.IsNullOrEmpty(id) || !int.TryParse(id, out var iId))
			if (string.IsNullOrEmpty(id))
			{
				if (authorizationFilterContext == null)
				{
					context.Fail();
					return;
				}

				// Method or controller authorization can be by passed.
				if (authorizationFilterContext.Filters.Any(x => x is ByPassAuthorizationAttribute))
				{
					_identityService.BypassAuthorizationFilter(context, requirement);
					return;
				}

				context.Fail();
				return;
			}

			//// Add the newly found account to cache for faster querying.
			//_profileCacheService.Add(iId, account, LifeTimeConstant.ProfileCacheLifeTime);
			//_identityService.SetProfile(httpContext, account);
			context.Succeed(requirement);
		}
		#endregion
	}
}
