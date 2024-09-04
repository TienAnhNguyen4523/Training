using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.ViewModel.User
{
	public class StatisticViewModel
	{
		public int TotalBorrowedBooks { get; set; }
		public int TotalFineAmount { get; set; }
		public int TotalSoldBooks { get; set; }
		public int TotalSoldMoney { get; set; }
		public Stream ExcelFile { get; set; }
		public Stream PdfFile { get; set; }
	}
}
