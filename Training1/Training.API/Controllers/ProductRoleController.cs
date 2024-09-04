using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.ProductRoles;
using Tranining.Domain.Service.Interface.IProductRoleService;


namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductRoleController : BaseController
	{
		private readonly IProductRoleService _ProductRoleService;
		private readonly IHttpContextAccessor _contextAccessor;
		public ProductRoleController(IProductRoleService ProductRoleServices, IHttpContextAccessor httpContextAccessor)
		{
			this._ProductRoleService = ProductRoleServices;
			_contextAccessor = httpContextAccessor;
		}
		[HttpPost("createproductRole")]
		
		public async Task<IActionResult> CreateProductRole([FromBody] CreateProductRoleCommand model)
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
				model = new CreateProductRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _ProductRoleService.CreateProductRole(model);
			return Ok(createResult);
		}
		[HttpDelete("deletepoductRole")]
		public async Task<IActionResult> DeleteProductRole([FromBody] DeleteProductRoleCommand model)
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
				model = new DeleteProductRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _ProductRoleService.DeleteProductRole(model);
			return Ok(createResult);
		}
		[HttpPut("updatepoductRole")]
		[AllowAnonymous]
		public async Task<IActionResult> UpdateProductRole([FromBody] UpdateProductRoleCommand model)
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
				model = new UpdateProductRoleCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _ProductRoleService.UpdateProductRole(model);
			return Ok(createResult);
		}
	}
}
