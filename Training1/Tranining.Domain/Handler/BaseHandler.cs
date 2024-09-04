using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;

namespace Tranining.Domain.Handler
{
	public abstract class BaseHandler
	{
		private readonly IHttpContextAccessor _httpContext;
		protected readonly IUnitOfWork _unitOfWork;
		public BaseHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
		public BaseHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) {
			_unitOfWork = unitOfWork;
			_httpContext = httpContext;
		}

		public Guid GetCurrentUserId()
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			if (identity == null) { return Guid.Empty; }
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("Id"));
			return Guid.Parse(userClaim?.Value);
		}
	
		public string GetCurrentUserName()
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("Name"));
			return userClaim?.Value;
		}

		public string GetCurrentUser()
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("UserId"));
			return userClaim?.Value;
		}

		public List<string> GetCurrentRoles()
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var roleClaims = identity?.Claims.Where(x => x.Type.Equals("roleName")).Select(x => x.Value).ToList();
			return roleClaims ?? new List<string>();
		}

		public string GetCurrentPermission()
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("Permissions"));
			return userClaim?.Value;
		}
	}
}
