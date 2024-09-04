using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Books;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.Author;
using Tranining.Domain.Service.Interface.Book;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : BaseController
	{
		private readonly IBookService _bookService;
		private readonly IHttpContextAccessor _contextAccessor;
		public BookController(IBookService bookServices, IHttpContextAccessor contextAccessor)
		{
			this._bookService = bookServices;
			_contextAccessor = contextAccessor;
		}
		//[HttpGet("getallbook")]
		
		[HttpPost("createbook")]
		public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand model)
		{
			if(CheckStandardUsers(_contextAccessor)) {
				throw new ArgumentException("U cant access to this feature");
			}
			if (!GetCurrentPermission(_contextAccessor).Contains("create"))
			{
				throw new ArgumentException("You have no permission");
			}

			#region Parameters validation
			if (model == null)
			{
				model = new CreateBookCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _bookService.CreateBook(model);
			return Ok(createResult);
		}
		[HttpDelete("deletebook")]
		public async Task<IActionResult> DeleteBook([FromBody] DeleteBookCommand model)
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
				model = new DeleteBookCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			model.CurrentUserId = GetCurrentUserId(_contextAccessor);
			model.CurrentRole = GetCurrentRoles(_contextAccessor);
			var createResult = await _bookService.DeleteBook(model);
			return Ok(createResult);
		}
		[HttpPut("updatebook")]
		public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand model)
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
				model = new UpdateBookCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			model.CurrentUserId = GetCurrentUserId(_contextAccessor);
			model.CurrentRole = GetCurrentRoles(_contextAccessor);
			var createResult = await _bookService.UpdateBook(model);
			return Ok(createResult);
		}
		[HttpPost("listing")]
		public async Task<IActionResult> Listing([FromBody] ListingBookCommand model)
		{
			if (!GetCurrentPermission(_contextAccessor).Contains("view"))
			{
				throw new ArgumentException("You have no permission");
			}
			model.UserRoles = GetCurrentRoles(_contextAccessor);
			var result = await _bookService.Listing(model);
			return Ok(result);
		}
	}
}
