using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.ViewModel.User
{
	public class AuthorViewModel
	{
		public Guid AuthorId { get; set; }
		public string AuthorName { get; set; }
	}
	public class CategoryViewModel
	{
		public Guid CategoryId { get; set; }
		public string CategoryName { get; set; }
	}
}
