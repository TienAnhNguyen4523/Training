using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.Books
{
	public class UpdateBookCommand : IRequest<bool>
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
		[JsonIgnore]
		public Guid CurrentUserId { get; set; }
		[JsonIgnore]
		public List<string> CurrentRole { get; set; } = new List<string>();
	}
}
