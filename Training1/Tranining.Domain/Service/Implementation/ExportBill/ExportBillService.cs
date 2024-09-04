using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.ExportBills;
using Tranining.Domain.Service.Interface.ExportBill;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Implementation.ExportBill
{
	public class ExportBillService : IExportBillService
	{
		private readonly IMediator _mediator;
		public ExportBillService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateExportBill(CreateExportBillCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateExportBill(UpdateExportBillCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteExportBill(DeleteExportBillCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<PaginationSet<ExportBillViewModel>> ListingExportBill(ListingExportBillCommand request)
		{
			try
			{
				return await _mediator.Send(request);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.ToString());
			}
		}
	}
}
