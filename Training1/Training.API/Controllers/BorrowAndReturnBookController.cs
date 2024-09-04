using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.BorrowAndReturnBooks;
using Tranining.Domain.Service.Interface.BorrowAndReturnBook;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BorrowAndReturnBookController : BaseController
	{
		private readonly IBorrowAndReturnBookService _BorrowAndReturnBookService;
		private readonly IHttpContextAccessor _contextAccessor;
		public BorrowAndReturnBookController(IBorrowAndReturnBookService BorrowAndReturnBookServices, IHttpContextAccessor contextAccessor)
		{
			this._BorrowAndReturnBookService = BorrowAndReturnBookServices;
			_contextAccessor = contextAccessor;
		}
		[HttpPost("createBorrowAndReturnBook")]
		public async Task<IActionResult> CreateBorrowAndReturnBook([FromBody] CreateBorrowAndReturnBookCommand model)
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
				model = new CreateBorrowAndReturnBookCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _BorrowAndReturnBookService.CreateBorrowAndReturnBook(model);
			return Ok(createResult);
		}
		[HttpPut("updateBorrowAndReturnBook")]
		public async Task<IActionResult> UpdateBorrowAndReturnBook([FromBody] UpdateBorrowAndReturnBookCommand model)
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
				model = new UpdateBorrowAndReturnBookCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _BorrowAndReturnBookService.UpdateBorrowAndReturnBook(model);
			return Ok(createResult);
		}
		[HttpDelete("deleteBorrowAndReturnBook")]
		public async Task<IActionResult> DeleteBorrowAndReturnBook([FromBody] DeleteBorrowAndReturnBookCommand model)
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
				model = new DeleteBorrowAndReturnBookCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _BorrowAndReturnBookService.DeleteBorrowAndReturnBook(model);
			return Ok(createResult);
		}
		[HttpPost("listingBorrowAndReturnBook")]
		public async Task<IActionResult> ListingBorrowAndReturnBook([FromBody] ListingBorrowAndReturnBookCommand model)
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
				model = new ListingBorrowAndReturnBookCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _BorrowAndReturnBookService.ListingBorrowAndReturnBook(model);
			return Ok(createResult);
		}
	}
}
