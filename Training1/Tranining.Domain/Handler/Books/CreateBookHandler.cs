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
	public class CreateBookHandler : BaseHandler,IRequestHandler<CreateBookCommand,bool>
	{
		public CreateBookHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			var Id = new Guid();
			var author = await _unitOfWork.Author.GetOne(x => x.AuthorName == request.AuthorName);
			if(author == null)
			{
				throw new ArgumentException("Not Found");
			}
			var category = await _unitOfWork.Category.GetOne(x => x.CategoryName == request.CategoryName);
			var book = new Book
			{
				Id = Id,
				BookName = request.BookName,
				Title = request.Title,
				Content = request.Content,
				YearOfRelease = request.YearOfRelease,
				Producer = request.Producer,
				Quantity = request.Quantity,
				Price = request.Price,
				Remaining = request.Remaining,
				Status = request.Status,
				Author = author,
				Category = category,
			};
			_unitOfWork.Book.Insert(book);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
