using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.ViewModel.User
{
	public class BookViewModel
	{
		public Guid Id { get; set; }
		public string BookName { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime YearOfRelease { get; set; }
		public string Producer { get; set; }
		public int Quantity { get; set; }
		public long Price { get; set; }
		public int Remaining { get; set; }
		public string Status { get; set; }
		public string AuthorName { get; set; }
		public string CategoryName { get; set; }
		public bool HasAccess { get; set; } = false;

	}
	public class ProductRoleViewModel
	{
		public Guid Id { get; set; }
		public string ListRole;
		
		
	}
}
