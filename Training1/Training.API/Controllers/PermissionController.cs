using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.Permissions;
using Tranining.Domain.Service.Interface.Permission;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PermissionController : BaseController
	{
		private readonly IPermissionService _PermissionService;
		private readonly IHttpContextAccessor _contextAccessor;
		public PermissionController(IPermissionService PermissionServices, IHttpContextAccessor contextAccessor)
		{
			this._PermissionService = PermissionServices;
			_contextAccessor = contextAccessor;
		}

		[HttpPost("createpermission")]
		public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand model)
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
				model = new CreatePermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _PermissionService.CreatePermission(model);
			return Ok(createResult);
		}
		[HttpPut("updatepermission")]
		public async Task<IActionResult> UpdatePermission([FromBody] UpdatePermissionCommand model)
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
				model = new UpdatePermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _PermissionService.UpdatePermission(model);
			return Ok(createResult);
		}
		[HttpDelete("deletepermission")]
		public async Task<IActionResult> DeletePermission([FromBody] DeletePermissionCommand model)
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
				model = new DeletePermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _PermissionService.DeletePermission(model);
			return Ok(createResult);
		}
		[HttpPost("listingpermission")]
		public async Task<IActionResult> ListingPermission([FromBody] ListingPermissionCommand model)
		{
			if (CheckStandardUsers(_contextAccessor))
			{
				throw new ArgumentException("U cant access to this feature");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("view"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new ListingPermissionCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _PermissionService.ListingPermission(model);
			return Ok(createResult);
		}
	}
}
