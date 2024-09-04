using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Books;
using Tranining.Domain.Command.BorrowAndReturnBooks;
using Tranining.Domain.Service.Interface.BorrowAndReturnBook;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Implementation.BorrowAndReturnBook
{
	public class BorrowAndReturnBookService : IBorrowAndReturnBookService
	{
		private readonly IMediator _mediator;
		public BorrowAndReturnBookService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateBorrowAndReturnBook(CreateBorrowAndReturnBookCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateBorrowAndReturnBook(UpdateBorrowAndReturnBookCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteBorrowAndReturnBook(DeleteBorrowAndReturnBookCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<PaginationSet<BorrowAndReturnBookViewModel>> ListingBorrowAndReturnBook(ListingBorrowAndReturnBookCommand request)
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
