using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.BorrowAndReturnBooks;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Interface.BorrowAndReturnBook
{
	public interface IBorrowAndReturnBookService
	{
		Task<bool> CreateBorrowAndReturnBook(CreateBorrowAndReturnBookCommand request);
		Task<bool> UpdateBorrowAndReturnBook(UpdateBorrowAndReturnBookCommand request);
		Task<bool> DeleteBorrowAndReturnBook(DeleteBorrowAndReturnBookCommand request);
		Task<PaginationSet<BorrowAndReturnBookViewModel>> ListingBorrowAndReturnBook(ListingBorrowAndReturnBookCommand request);
	}
}
