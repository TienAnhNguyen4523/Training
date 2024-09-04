using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.ExportBills;

namespace Tranining.Domain.Handler.ExportBills
{
	public class CreateExportBillHandler : BaseHandler,IRequestHandler<CreateExportBillCommand,bool>
	{
		public CreateExportBillHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateExportBillCommand request, CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Book.GetById(request.BookId);
			var user = await _unitOfWork.User.GetById(request.UserId);
			if (book == null || user == null)
			{
				throw new ArgumentException("Not Found");
			}
			if (book.Quantity < request.Quantity)
			{
				throw new ArgumentException("Error");
			}

			var Id = Guid.NewGuid();
			var exportBill = new ExportBill
			{
				Id = Id,
				BookId = book.Id,
				UserId = user.Id,
				Quantity = request.Quantity,
				Price = request.Price,
				Note = request.Note,
				Librarian = request.Librarian
			};
			book.Quantity = book.Quantity - request.Quantity;
			_unitOfWork.Book.Update(book);
			_unitOfWork.ExportBill.Insert(exportBill);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
