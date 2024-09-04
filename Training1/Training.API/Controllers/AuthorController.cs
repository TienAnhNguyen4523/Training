using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Implementation.Role;
using Tranining.Domain.Service.Interface.Author;
using Tranining.Domain.Service.Interface.Role;
using Tranining.Domain.Service.Interface.UserRole;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : BaseController
	{
		private readonly IAuthorService _authorService;
		private readonly IHttpContextAccessor _contextAccessor;
		public AuthorController(IAuthorService authorServices, IHttpContextAccessor contextAccessor)
		{
			this._authorService = authorServices;
			_contextAccessor = contextAccessor;
		}
		[HttpPost("createauthor")]	
		public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorCommand model)
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
				model = new CreateAuthorCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _authorService.CreateAuthor(model);
			return Ok(createResult);
		}
		[HttpPut("updateauthor")]
		public async Task<IActionResult> DeleteAuthor([FromBody] UpdateAuthorCommand model)
		{
			if (CheckStandardUsers(_contextAccessor))
			{
				throw new ArgumentException("You can't access to this feature");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("update"))
			{
				throw new ArgumentException("You have no permission");
			}
			#region Parameters validation
			if (model == null)
			{
				model = new UpdateAuthorCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			model.CurrentUserId = GetCurrentUserId(_contextAccessor);
			model.CurrentRole = GetCurrentRoles(_contextAccessor);
			var updateResult = await _authorService.UpdateAuthor(model);
			return Ok(updateResult);
		}
		[HttpDelete("deleteauthor")]
		public async Task<IActionResult> DeleteAuthor([FromBody] DeleteAuthorCommand model)
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
				model = new DeleteAuthorCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			model.CurrentUserId = GetCurrentUserId(_contextAccessor);
			model.CurrentRole = GetCurrentRoles(_contextAccessor);
			var deleteResult = await _authorService.DeleteAuthor(model);
			return Ok(deleteResult);
		}
		[HttpPost("listingauthor")]
		public async Task<IActionResult> Listing([FromBody] ListingAuthorCommand model)
		{
			if (!GetCurrentPermission(_contextAccessor).Contains("view"))
			{
				throw new ArgumentException("You have no permission");
			}
			var result = await _authorService.Listing(model);
			return Ok(result);
		}

	}
}
