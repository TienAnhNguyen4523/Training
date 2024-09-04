using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.Permissions;
using Tranining.Domain.Command.UserPermissions;
using Tranining.Domain.Service.Interface.Permission;
using Tranining.Domain.Service.Interface.UserPermission;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserPermissionController : BaseController
	{
		private readonly IUserPermissionService _UserPermissionService;
		private readonly IHttpContextAccessor _contextAccessor;
		public UserPermissionController(IUserPermissionService UserPermissionServices, IHttpContextAccessor contextAccessor)
		{
			this._UserPermissionService = UserPermissionServices;
			_contextAccessor = contextAccessor;
		}

		[HttpPost("createuserpermission")]
		public async Task<IActionResult> CreateUserPermission([FromBody] CreateUserPermissionCommand model)
		{
			if (CheckStandardUsers(_contextAccessor))
			{
				throw new ArgumentException("U cant access to this feature");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("create"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new CreateUserPermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			model.ListRole = GetCurrentRoles(_contextAccessor);
			#endregion
			var createResult = await _UserPermissionService.CreateUserPermission(model);
			return Ok(createResult);
		}
		[HttpPut("updateserpermission")]
		public async Task<IActionResult> UpdateUserPermission([FromBody] UpdateUserPermissionCommand model)
		{
			if (CheckStandardUsers(_contextAccessor))
			{
				throw new ArgumentException("U cant access to this feature");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("update"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new UpdateUserPermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _UserPermissionService.UpdateUserPermission(model);
			return Ok(createResult);
		}
		[HttpDelete("updateserpermission")]
		public async Task<IActionResult> DeleteUserPermission([FromBody] DeleteUserPermissionCommand model)
		{
			if (CheckStandardUsers(_contextAccessor))
			{
				throw new ArgumentException("U cant access to this feature");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("delete"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new DeleteUserPermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _UserPermissionService.DeleteUserPermission(model);
			return Ok(createResult);
		}
		[HttpPost("updateserpermission")]
		public async Task<IActionResult> ListingUserPermission([FromBody] ListingUserPermissionCommand model)
		{
			if (CheckStandardUsers(_contextAccessor))
			{
				throw new ArgumentException("U cant access to this feature");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new ListingUserPermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _UserPermissionService.ListingUserPermission(model);
			return Ok(createResult);
		}
	}
}
