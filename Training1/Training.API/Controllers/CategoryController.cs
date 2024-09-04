using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Service.Interface.Author;
using Tranining.Domain.Service.Interface.Category;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : BaseController
	{
		private readonly ICategoryService _categoryService;
		private readonly IHttpContextAccessor _contextAccessor;
		public CategoryController(ICategoryService categoryServices, IHttpContextAccessor contextAccessor)
		{
			this._categoryService = categoryServices;
			_contextAccessor = contextAccessor;
		}
		[HttpPost("createcategory")]
		
		public async Task<IActionResult> CreateAuthor([FromBody] CreateCategoryCommand model)
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
				model = new CreateCategoryCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _categoryService.CreateCategory(model);
			return Ok(createResult);
		}
		[HttpPut("updatecategory")]
		public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand model)
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
				model = new UpdateCategoryCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			model.CurrentUserId = GetCurrentUserId(_contextAccessor);
			model.CurrentRole = GetCurrentRoles(_contextAccessor);
			var updateResult = await _categoryService.UpdateCategory(model);
			return Ok(updateResult);
		}
		[HttpDelete("deletecategory")]

		public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand model)
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
				model = new DeleteCategoryCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			model.CurrentUserId = GetCurrentUserId(_contextAccessor);
			model.CurrentRole = GetCurrentRoles(_contextAccessor);
			var deleteResult = await _categoryService.DeleteCategory(model);
			return Ok(deleteResult);
		}
		[HttpPost("listingcategory")]
		public async Task<IActionResult> Listing([FromBody] ListingCategoryCommand model)
		{
			if (!GetCurrentPermission(_contextAccessor).Contains("view"))
			{
				throw new ArgumentException("You have no permission");
			}
			var result = await _categoryService.Listing(model);
			return Ok(result);
		}
	}
}
