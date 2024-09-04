using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Books;

namespace Tranining.Domain.Handler.Books
{
	public class UpdateBookHandler : BaseHandler,IRequestHandler<UpdateBookCommand,bool>
	{
		public UpdateBookHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
		{
			var CurrentUserId = request.CurrentUserId;
			var roles = request.CurrentRole;
			var listRole = String.Join(",", roles);
			var book = await _unitOfWork.Book.GetOne(x => x.Id == request.Id);
			if (book == null)
			{
				throw new ArgumentException("Not Found");
			}
			if(CurrentUserId != book.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			var author = await _unitOfWork.Author.GetOne(x => x.AuthorName == request.AuthorName);

			var category = await _unitOfWork.Category.GetOne(x => x.CategoryName == request.CategoryName);

			book.Id = request.Id;
			book.BookName = request.BookName;
			book.Title = request.Title;
			book.Content = request.Content;
			book.YearOfRelease = request.YearOfRelease;
			book.Producer = request.Producer;
			book.Quantity = request.Quantity;
			book.Price = request.Price;
			book.Remaining = request.Remaining;
			book.Status = request.Status;
			book.Author = author;
			book.Category = category;
			_unitOfWork.Book.Update(book);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}

}
