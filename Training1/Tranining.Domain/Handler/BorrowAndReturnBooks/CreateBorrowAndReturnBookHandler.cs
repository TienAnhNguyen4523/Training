using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.BorrowAndReturnBooks;

namespace Tranining.Domain.Handler.BorrowAndReturnBooks
{
	public class CreateBorrowAndReturnBookHandler : BaseHandler,IRequestHandler<CreateBorrowAndReturnBookCommand,bool>
	{
		public CreateBorrowAndReturnBookHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateBorrowAndReturnBookCommand request,CancellationToken cancellationToken)
		{
			
			var book = await _unitOfWork.Book.GetById(request.BookId);
			var user = await _unitOfWork.User.GetById(request.UserId);
			if (book == null || user == null)
			{
				throw new ArgumentException("Not Found");
			}
			var bookUser = await _unitOfWork.ProductRole.GetOne(x => x.BookId == book.Id);
			var userRole = await _unitOfWork.UserRole.GetOne(x=>x.UserId == user.Id);
			if (!bookUser.ListRole.Contains(userRole.Role.RoleName))
			{
				throw new ArgumentException("This user cant access to this book");
			}
			if(book.Quantity < request.Quantity)
			{
				throw new ArgumentException("Error");
			}
			
			var Id = Guid.NewGuid();
			var BorrowAndReturnBook = new BorrowAndReturnBook
			{
				Id = Id,
				BookId = book.Id,
				UserId = user.Id,
				Quantity = request.Quantity,
				StartDate = request.StartDate,
				EndDate = request.EndDate,
				Note = request.Note,
				FineAmount = request.FineAmount,
				Librarian = request.Librarian,
				IsReturn = request.IsReturn
			};
			book.Quantity = book.Quantity - request.Quantity;
			_unitOfWork.Book.Update(book);
			_unitOfWork.BorrowAndReturnBook.Insert(BorrowAndReturnBook);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
