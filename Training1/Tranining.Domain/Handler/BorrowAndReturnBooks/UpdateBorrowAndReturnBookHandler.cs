using MediatR;
using Microsoft.AspNetCore.Http;
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
	public class UpdateBorrowAndReturnBookHandler : BaseHandler,IRequestHandler<UpdateBorrowAndReturnBookCommand,bool>
	{
		//public UpdateBorrowAndReturnBookHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public UpdateBorrowAndReturnBookHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : base(unitOfWork, httpContext)
		{
		}
		public async Task<bool> Handle(UpdateBorrowAndReturnBookCommand request,CancellationToken cancellationToken)
		{
			var borrowAndReturnBook = await _unitOfWork.BorrowAndReturnBook.GetById(request.Id);
			var listRole = String.Join(",", GetCurrentRoles());
			if (GetCurrentUserId() != borrowAndReturnBook.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			if (borrowAndReturnBook == null) {
				throw new ArgumentException("Not Found");
			}
			var book = await _unitOfWork.Book.GetOne(x => x.Id == borrowAndReturnBook.BookId);
			var user = await _unitOfWork.User.GetOne(x => x.Id == borrowAndReturnBook.UserId);
			var bookUser = await _unitOfWork.ProductRole.GetOne(x => x.BookId == book.Id);
			var userRole = await _unitOfWork.UserRole.GetOne(x => x.UserId == user.Id);
			if (!bookUser.ListRole.Contains(userRole.Role.RoleName))
			{
				throw new ArgumentException("This user cant access to this book");
			}
			//borrowAndReturnBook.Id = request.Id;
			//borrowAndReturnBook.BookId = book.Id;
			//borrowAndReturnBook.UserId = user.Id;
			//borrowAndReturnBook.Quantity = request.Quantity;
			//borrowAndReturnBook.StartDate = request.StartDate;
			//borrowAndReturnBook.EndDate = request.EndDate;
			//borrowAndReturnBook.Note = request.Note;
			//borrowAndReturnBook.FineAmount = request.FineAmount;
			//borrowAndReturnBook.Librarian = request.Librarian;
			borrowAndReturnBook.IsReturn = request.IsReturn;
			_unitOfWork.BorrowAndReturnBook.Update(borrowAndReturnBook);
			if (request.IsReturn == true)
			{
				book.Quantity = book.Quantity + borrowAndReturnBook.Quantity;
				if (borrowAndReturnBook.UpdatedDate.HasValue)
				{
					if (borrowAndReturnBook.EndDate >= borrowAndReturnBook.UpdatedDate.Value)
					{
						borrowAndReturnBook.FineAmount = 0;
					}
					else
					{
						TimeSpan lateDays = borrowAndReturnBook.UpdatedDate.Value - borrowAndReturnBook.EndDate;
						borrowAndReturnBook.FineAmount *= lateDays.Days;
					}
				}			
			}
			_unitOfWork.Book.Update(book);
			_unitOfWork.BorrowAndReturnBook.Update(borrowAndReturnBook);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
