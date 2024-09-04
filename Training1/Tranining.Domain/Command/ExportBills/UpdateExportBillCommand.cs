using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranining.Domain.Command.ExportBills
{
	public class UpdateExportBillCommand : IRequest<bool>
	{
		[Required]
		public Guid Id { get; set; }
		[Required]
		public Guid BookId { get; set; }
		[Required]
		public Guid UserId { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public int Price { get; set; }
		public string Note { get; set; }
		[Required]
		public Guid Librarian { get; set; }
	}
}
