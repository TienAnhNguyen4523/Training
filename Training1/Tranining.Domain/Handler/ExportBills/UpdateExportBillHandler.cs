using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.ExportBills;

namespace Tranining.Domain.Handler.ExportBills
{
	public class UpdateExportBillHandler : BaseHandler,IRequestHandler<UpdateExportBillCommand,bool>
	{
		//public UpdateExportBillHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public UpdateExportBillHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : base(unitOfWork, httpContext)
		{
		}
		public async Task<bool> Handle(UpdateExportBillCommand request,CancellationToken cancellationToken)
		{
			var exportBill = await _unitOfWork.ExportBill.GetById(request.Id);
			var listRole = String.Join(",", GetCurrentRoles());

			if (GetCurrentUserId() != exportBill.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			if (exportBill == null)
			{
				throw new ArgumentException("Not Found");
			}
			var book = await _unitOfWork.Book.GetById(request.BookId);
			var user = await _unitOfWork.User.GetById(request.UserId);
			if (book == null || user == null)
			{
				throw new ArgumentException("Not Found");
			}

			exportBill.Id = request.Id;
			exportBill.BookId = book.Id;
			exportBill.UserId = user.Id;
			exportBill.Quantity = request.Quantity;
			exportBill.Price = request.Price;
			exportBill.Note = request.Note;
			exportBill.Librarian = request.Librarian;

			_unitOfWork.ExportBill.Update(exportBill);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
