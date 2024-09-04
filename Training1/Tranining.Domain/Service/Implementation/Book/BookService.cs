using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Books;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.Book;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Implementation.Book
{
	public class BookService : IBookService
	{
		private readonly IMediator _mediator;
		public BookService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateBook(CreateBookCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteBook(DeleteBookCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateBook(UpdateBookCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<PaginationSet<BookViewModel>> Listing(ListingBookCommand request)
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
