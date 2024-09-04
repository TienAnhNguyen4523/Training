using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Handler;
using Tranining.Domain.Handler.ExportBills;

namespace Tranining.Domain.Command.ExportBills
{
	public class DeleteExportBillCommand : IRequest<bool>
	{
		[Required]
		public Guid Id { get; set; }
	}
}
