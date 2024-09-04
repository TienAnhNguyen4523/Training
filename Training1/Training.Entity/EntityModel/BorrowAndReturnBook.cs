using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Entity.EntityModel
{
	[Table("BorrowAndReturnBook")]
	public class BorrowAndReturnBook : EntityBase
	{
		[Required]
		public Guid BookId { get;set; }
		[Required]
		public Guid UserId { get;set; }
		[Required]
		public int Quantity { get;set; }
		[Required]
		public DateTime StartDate { get;set; }
		[Required]
		public DateTime EndDate { get;set; }
		public string Note { get;set; }
		[Required]
		public int FineAmount { get;set; }
		[Required]
		public Guid Librarian { get;set; }
		[Required]
		public bool IsReturn { get;set; }
		public virtual Book Book { get; set; }
		public virtual User User { get; set; }
	}
}
