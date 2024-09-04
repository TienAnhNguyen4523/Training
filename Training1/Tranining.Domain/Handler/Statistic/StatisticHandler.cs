using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Statistic;
using Tranining.Domain.Handler;
using Tranining.Domain.ViewModel.User;
using MediatR;
using OfficeOpenXml;

public class StatisticHandler : BaseHandler, IRequestHandler<StatisticCommand, StatisticViewModel>
{
	public StatisticHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

	public async Task<StatisticViewModel> Handle(StatisticCommand request, CancellationToken cancellationToken)
	{
		if (request.Season != null || request.Year != null || request.Month != null)
		{
			(request.StartDate, request.EndDate) = GetStartAndEndDate(request);
		}

		var borrowAndReturnBooks = _unitOfWork.BorrowAndReturnBook.GetQueryable();
		var totalBorrowedBooks = from borrowAndReturnBook in borrowAndReturnBooks
								 where borrowAndReturnBook.IsReturn == true && borrowAndReturnBook.CreatedDate >= request.StartDate && borrowAndReturnBook.CreatedDate <= request.EndDate
								 select new { borrowAndReturnBook.Quantity, borrowAndReturnBook.FineAmount };
		var exportBills = _unitOfWork.ExportBill.GetQueryable();
		var totalSoldBooks = from exportBill in exportBills
							 where exportBill.CreatedDate >= request.StartDate && exportBill.CreatedDate <= request.EndDate
							 select new { exportBill.Quantity, exportBill.Price };
		var query = new StatisticViewModel
		{
			TotalBorrowedBooks = totalBorrowedBooks.Sum(b => b.Quantity),
			TotalFineAmount = totalBorrowedBooks.Sum(b => b.FineAmount),
			TotalSoldBooks = totalSoldBooks.Sum(e => e.Quantity),
			TotalSoldMoney = totalSoldBooks.Sum(e => e.Quantity * e.Price)
		};

		// Tạo file Excel từ thống kê
		var excelStream = CreateExcelFile(query);

		// Tạo file PDF từ thống kê
		var pdfStream = CreatePdfFile(query);

		// Trả về kết quả thống kê và file Excel, PDF
		query.ExcelFile = excelStream;
		query.PdfFile = pdfStream;
		return query;
	}

	private (DateTime StartDate, DateTime EndDate) GetStartAndEndDate(StatisticCommand request)
	{
		if (request.Season.HasValue)
		{
			return request.Season switch
			{
				1 => (new DateTime(request.Year.Value, 1, 1), new DateTime(request.Year.Value, 3, 31)),
				2 => (new DateTime(request.Year.Value, 4, 1), new DateTime(request.Year.Value, 6, 30)),
				3 => (new DateTime(request.Year.Value, 7, 1), new DateTime(request.Year.Value, 9, 30)),
				4 => (new DateTime(request.Year.Value, 10, 1), new DateTime(request.Year.Value, 12, 31)),
				_ => throw new ArgumentException("Invalid season value.")
			};
		}
		else if (request.Month.HasValue)
		{
			int daysInMonth = DateTime.DaysInMonth(request.Year.Value, request.Month.Value);
			return (new DateTime(request.Year.Value, request.Month.Value, 1), new DateTime(request.Year.Value, request.Month.Value, daysInMonth));
		}

		throw new ArgumentException("Either Season or Month must be specified.");
	}

	private Stream CreateExcelFile(StatisticViewModel data)
	{
		using (var excelPackage = new ExcelPackage())
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			excelPackage.Workbook.Properties.Author = "M4Y";
			excelPackage.Workbook.Properties.Title = "Statistic Report";

			var workSheet = excelPackage.Workbook.Worksheets.Add("Statistics");

			workSheet.Cells[1, 1].Value = "Total Borrowed Books";
			workSheet.Cells[1, 2].Value = data.TotalBorrowedBooks;
			workSheet.Cells[2, 1].Value = "Total Fine Amount";
			workSheet.Cells[2, 2].Value = data.TotalFineAmount;
			workSheet.Cells[3, 1].Value = "Total Sold Books";
			workSheet.Cells[3, 2].Value = data.TotalSoldBooks;
			workSheet.Cells[4, 1].Value = "Total Sold Money";
			workSheet.Cells[4, 2].Value = data.TotalSoldMoney;

			var stream = new MemoryStream();
			excelPackage.SaveAs(stream);
			stream.Position = 0;
			return stream;
		}
	}

	private Stream CreatePdfFile(StatisticViewModel data)
	{
		var document = new PdfDocument();
		var page = document.AddPage();
		var gfx = XGraphics.FromPdfPage(page);
		var font = new XFont("Verdana", 12);

		gfx.DrawString("Statistic Report", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height),
			XStringFormats.TopCenter);

		gfx.DrawString($"Total Borrowed Books: {data.TotalBorrowedBooks}", font, XBrushes.Black,
			new XRect(40, 60, page.Width, page.Height), XStringFormats.TopLeft);
		gfx.DrawString($"Total Fine Amount: {data.TotalFineAmount}", font, XBrushes.Black,
			new XRect(40, 90, page.Width, page.Height), XStringFormats.TopLeft);
		gfx.DrawString($"Total Sold Books: {data.TotalSoldBooks}", font, XBrushes.Black,
			new XRect(40, 120, page.Width, page.Height), XStringFormats.TopLeft);
		gfx.DrawString($"Total Sold Money: {data.TotalSoldMoney}", font, XBrushes.Black,
			new XRect(40, 150, page.Width, page.Height), XStringFormats.TopLeft);

		var stream = new MemoryStream();
		document.Save(stream, false);
		stream.Position = 0;

		return stream;
	}
}
