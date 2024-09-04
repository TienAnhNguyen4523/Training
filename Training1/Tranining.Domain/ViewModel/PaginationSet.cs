using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.ViewModel
{
	public class PaginationSet<T>
	{
		public int PageIndex { get; set; }
		public int Count { get; set; }
		public int TotalCount { get; set; }
		public int ToltalPages { get; set; }
		public List<T> Items { get; set; }
	}
}
