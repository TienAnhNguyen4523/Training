using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tranining.Domain.Command.Statistic;
using Tranining.Domain.Service.Interface.Statistic;
using System.IO;
using System.Threading.Tasks;

namespace Training.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatisticController : BaseController
	{
		private readonly IStatisticService _StatisticService;
		private readonly IHttpContextAccessor _contextAccessor;
		public StatisticController(IStatisticService StatisticServices, IHttpContextAccessor contextAccessor)
		{
			this._StatisticService = StatisticServices;
			_contextAccessor = contextAccessor;
		}

		[HttpPost("createStatistic")]
		public async Task<IActionResult> CreateStatistic([FromBody] StatisticCommand model, [FromQuery] string exportType = "none")
		{
			#region Parameters validation
			if (model == null)
			{
				model = new StatisticCommand();
				TryValidateModel(model);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			#endregion

			var createResult = await _StatisticService.RevenueStatistic(model);

			if (exportType == "excel")
			{
				var stream = createResult.ExcelFile as MemoryStream;

				if (stream == null)
					return BadRequest("Unable to generate Excel file.");

				var content = stream.ToArray();
				var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				var fileName = "StatisticReport.xlsx";

				return File(content, contentType, fileName);
			}
			else if (exportType == "pdf")
			{
				var stream = createResult.PdfFile as MemoryStream;

				if (stream == null)
					return BadRequest("Unable to generate PDF file.");

				var content = stream.ToArray();
				var contentType = "application/pdf";
				var fileName = "StatisticReport.pdf";

				return File(content, contentType, fileName);
			}

			return Ok(createResult);
		}
	}
}
