using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Books;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Interface.Book
{
	public interface IBookService
	{
		//Task<IEnumerable<>> GetAllBooks();
		Task<bool> CreateBook(CreateBookCommand request);
		Task<bool> DeleteBook(DeleteBookCommand request);
		Task<bool> UpdateBook(UpdateBookCommand request);
		Task<PaginationSet<BookViewModel>> Listing(ListingBookCommand request);
	}
}
