using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("Book")]
	public class Book : EntityBase
	{
		[Required]
		public Guid AuthorId { get; set; }
		[Required]
		public Guid CategoryId { get; set; }
		public string BookName { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime YearOfRelease { get; set; }
		public string Producer { get; set; }
		public int Quantity { get; set; }
		public long Price { get; set; }
		public int Remaining { get; set; }
		public string Status { get; set; }
		public virtual ProductRole ProductRole { get; set; }
		public virtual Author Author { get; set; }
		public virtual Category Category { get; set; }
		//public virtual BorrowAndReturnBook BorrowAndReturnBook { get; set; }
		//public virtual ExportBill ExportBill { get; set; }
	}

}
