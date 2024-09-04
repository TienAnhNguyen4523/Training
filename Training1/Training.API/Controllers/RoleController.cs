using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.Role;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : BaseController
	{
		private readonly IRoleServices _roleServices;
		private readonly IHttpContextAccessor _contextAccessor;
		public RoleController(IRoleServices roleServices,IHttpContextAccessor httpContextAccessor)
		{
			this._roleServices = roleServices;
			_contextAccessor = httpContextAccessor;
		}
		[HttpPost("createrole")]
		public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand model)
		{
			if (!CheckAdmin(_contextAccessor))
			{
				throw new ArgumentException("U are not Admin");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("create"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new CreateRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _roleServices.CreateRole(model);
			return Ok(createResult);
		}
		[HttpPut("updaterole")]
		public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand model)
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
				model = new UpdateRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var updateResult = await _roleServices.UpdateRole(model);
			return Ok(updateResult);
		}
		[HttpDelete("deleterole")]
		public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleCommand model)
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
				model = new DeleteRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var deleteResult = await _roleServices.DeleteRole(model);
			return Ok(deleteResult);
		}
		[HttpPost("listingrole")]
		public async Task<IActionResult> Listing([FromBody] GetRolePaginationCommand model)
		{

			var result = await _roleServices.ListingRole(model);
			return Ok(result);
		}
	}
}
