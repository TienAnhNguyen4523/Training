using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.ExportBills;
using Tranining.Domain.Extensions;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Handler.ExportBills
{
	public class ListingExportBillHandler : BaseHandler,IRequestHandler<ListingExportBillCommand,PaginationSet<ExportBillViewModel>>
	{
		public ListingExportBillHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<ExportBillViewModel>> Handle(ListingExportBillCommand request,CancellationToken cancellationToken)
		{
			var exportBills = _unitOfWork.ExportBill.GetQueryable();
			var users = _unitOfWork.User.GetQueryable();
			var books = _unitOfWork.Book.GetQueryable();
			var query = from exportBill in exportBills
						join user in users on exportBill.UserId equals user.Id
						join book in books on exportBill.BookId equals book.Id
						select new ExportBillViewModel
						{
							Id = exportBill.Id,
							UserId = user.UserId,
							BookName = book.BookName,
							Quantity = exportBill.Quantity,
							Price = exportBill.Price,
							Note = exportBill.Note,							
							Librarian = exportBill.Librarian,
					
						};
			if (request.Filter != null)
			{
				query = query.Filter(request.Filter);
			}
			if (request.SearchString != null)
			{
				query = query.Search(request.SearchString);
			}

			var result = await query.Pagination(request.PageIndex, request.PageSize, request.Sort);
			return result;
		}
	}
}
