using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.ExportBills;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Interface.ExportBill
{
	public interface IExportBillService
	{
		Task<bool> CreateExportBill(CreateExportBillCommand request);
		Task<bool> UpdateExportBill(UpdateExportBillCommand request);
		Task<bool> DeleteExportBill(DeleteExportBillCommand request);
		Task<PaginationSet<ExportBillViewModel>> ListingExportBill(ListingExportBillCommand request);
	}
}
