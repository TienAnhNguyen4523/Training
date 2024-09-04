using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.ExportBills;
using Tranining.Domain.Service.Interface.ExportBill;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExportBillController : BaseController
	{
		private readonly IExportBillService _ExportBillService;
		private readonly IHttpContextAccessor _contextAccessor;
		public ExportBillController(IExportBillService ExportBillServices, IHttpContextAccessor contextAccessor)
		{
			this._ExportBillService = ExportBillServices;
			_contextAccessor = contextAccessor;
		}
		[HttpPost("createExportBill")]
		public async Task<IActionResult> CreateExportBill([FromBody] CreateExportBillCommand model)
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
				model = new CreateExportBillCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _ExportBillService.CreateExportBill(model);
			return Ok(createResult);
		}
		[HttpPut("updateExportBill")]
		public async Task<IActionResult> UpdateExportBill([FromBody] UpdateExportBillCommand model)
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
				model = new UpdateExportBillCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _ExportBillService.UpdateExportBill(model);
			return Ok(createResult);
		}
		[HttpDelete("deleteExportBill")]
		public async Task<IActionResult> DeleteExportBill([FromBody] DeleteExportBillCommand model)
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
				model = new DeleteExportBillCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _ExportBillService.DeleteExportBill(model);
			return Ok(createResult);
		}
		[HttpPost("listingExportBill")]
		public async Task<IActionResult> ListingExportBill([FromBody] ListingExportBillCommand model)
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
				model = new ListingExportBillCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion
			var createResult = await _ExportBillService.ListingExportBill(model);
			return Ok(createResult);
		}
	}
}
