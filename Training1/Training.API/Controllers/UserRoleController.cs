using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using Tranining.Domain.Command.UserRoles;
using Tranining.Domain.Service.Interface.UserRole;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserRoleController : BaseController
	{
		private readonly IUserRoleService _userRoleService;
		private readonly IHttpContextAccessor _contextAccessor;
		public UserRoleController(IUserRoleService userRoleService, IHttpContextAccessor contextAccessor)
		{
			_userRoleService = userRoleService;
			_contextAccessor = contextAccessor;
		}
		[HttpPost("createuserrole")]
		public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleCommand model)
		{
			if (!CheckAdmin(_contextAccessor))
			{
				throw new ArgumentException("This feature can't be accessed if u are not admin");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("create"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new CreateUserRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			
			var loginResult = await _userRoleService.CreateUserRole(model);
			return Ok(loginResult);
		}
		[HttpPut("updateuserrole")]
		public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommand model)
		{
			if (!CheckAdmin(_contextAccessor))
			{
				throw new ArgumentException("U are not Admin");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("update"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new UpdateUserRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var updateResult = await _userRoleService.UpdateUserRole(model);
			return Ok(updateResult);
		}
		[HttpDelete("deleteuserrole")]
		public async Task<IActionResult> DeleteUserRole([FromBody] DeleteUserRoleCommand model)
		{
			if (!CheckAdmin(_contextAccessor))
			{
				throw new ArgumentException("U are not Admin");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("delete"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new DeleteUserRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var deleteResult = await _userRoleService.DeleteUserRole(model);
			return Ok(deleteResult);
		}
	}
}
