using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		[ApiExplorerSettings(IgnoreApi = true)]
		public Guid GetCurrentUserId(IHttpContextAccessor _httpContext)
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			if (identity == null) { return Guid.Empty; }
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("Id"));
			return Guid.Parse(userClaim?.Value);
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public string GetCurrentUserName(IHttpContextAccessor _httpContext)
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("Name"));
			return userClaim?.Value;
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public string GetCurrentUser(IHttpContextAccessor _httpContext)
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("UserId"));
			return userClaim?.Value;
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public List<string> GetCurrentRoles(IHttpContextAccessor _httpContext)
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var roleClaims = identity?.Claims.Where(x => x.Type.Equals("roleName")).Select(x => x.Value).ToList();
			return roleClaims ?? new List<string>();
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public string GetCurrentPermission(IHttpContextAccessor _httpContext)
		{
			var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
			var userClaim = identity?.Claims.SingleOrDefault(x => x.Type.Equals("Permissions"));
			return userClaim?.Value;
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public bool CheckAdmin(IHttpContextAccessor _httpContext)
		{
			var check = false;
			foreach (var user in GetCurrentRoles(_httpContext))
			{
				if (user == "Admin")
				{
					check = true;
				}
			}
			return check;
		}
		
		[ApiExplorerSettings(IgnoreApi = true)]
		public bool CheckBookManager(IHttpContextAccessor _httpContext)
		{
			var check = false;
			foreach (var user in GetCurrentRoles(_httpContext))
			{
				if (user == "BookManager")
				{
					check = true;
				}
			}
			return check;
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public bool CheckStandardUsers(IHttpContextAccessor _httpContext)
		{
			var check = false;
			foreach (var user in GetCurrentRoles(_httpContext))
			{
				if (user == "StandardUsers")
				{
					check = true;
				}
			}
			return check;
		}
		
		

	}
}
