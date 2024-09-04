using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.ViewModel.User
{
	public class BorrowAndReturnBookViewModel
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string BookName { get; set; }
		[Required]
		public string UserId { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
		public string Note { get; set; }
		[Required]
		public int FineAmount { get; set; }
		[Required]
		public Guid Librarian { get; set; }
		[Required]
		public bool IsReturn { get; set; }
	}
}
