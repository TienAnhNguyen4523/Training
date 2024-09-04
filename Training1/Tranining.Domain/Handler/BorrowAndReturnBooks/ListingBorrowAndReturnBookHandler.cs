using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.BorrowAndReturnBooks;
using Tranining.Domain.Extensions;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Handler.BorrowAndReturnBooks
{
	public class ListingBorrowAndReturnBookHandler : BaseHandler,IRequestHandler<ListingBorrowAndReturnBookCommand,PaginationSet<BorrowAndReturnBookViewModel>>
	{
		public ListingBorrowAndReturnBookHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<BorrowAndReturnBookViewModel>> Handle(ListingBorrowAndReturnBookCommand request, CancellationToken cancellationToken)
		{
			var borrowAndReturnBooks = _unitOfWork.BorrowAndReturnBook.GetQueryable();
			var users = _unitOfWork.User.GetQueryable();
			var books = _unitOfWork.Book.GetQueryable();
			var query = from borrowAndReturnBook in borrowAndReturnBooks
						join user in users on borrowAndReturnBook.UserId equals user.Id
						join book in books on borrowAndReturnBook.BookId equals book.Id
						select new BorrowAndReturnBookViewModel
						{
							Id = borrowAndReturnBook.Id,
							UserId = user.UserId,
							BookName = book.BookName,
							Quantity = borrowAndReturnBook.Quantity,
							StartDate = borrowAndReturnBook.StartDate,
							EndDate = borrowAndReturnBook.EndDate,
							Note = borrowAndReturnBook.Note,
							FineAmount = borrowAndReturnBook.FineAmount,
							Librarian = borrowAndReturnBook.Librarian,
							IsReturn = borrowAndReturnBook.IsReturn
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
