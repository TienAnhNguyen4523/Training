using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Books;
using Tranining.Domain.Extensions;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Handler.Books
{
	public class ListingBookHandler : BaseHandler,IRequestHandler<ListingBookCommand,PaginationSet<BookViewModel>>
	{
		
		public ListingBookHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<BookViewModel>> Handle(ListingBookCommand request, CancellationToken cancellationToken)
		{

			var currentUserRoles = request.UserRoles;
			//var stringRole = String.Join(", ", currentUserRoles);
			//var roles = _unitOfWork.ProductRole.GetQueryable()
			//.Where(x => x.ListRole.Contains(stringRole));	
			var roles = await _unitOfWork.ProductRole.GetQueryable()
				.ToListAsync();
			//var matchingBookIds = roles
			//	.Where(x => x.ListRole.Split(',').Intersect(currentUserRoles).Any())
			//	.Select(x => x.Book.Id)
			//	.Distinct()
			//	.ToList();
			//var query = _unitOfWork.Book.GetQueryable().Where(x => matchingBookIds.Contains(x.Id));
			//var book = query
			//	.Select(x => new BookViewModel
			//	{
			//		Id = x.Id,
			//		BookName = x.BookName,
			//		Title = x.Title,
			//		Content = x.Content,
			//		YearOfRelease = x.YearOfRelease,
			//		Producer = x.Producer,
			//		Quantity = x.Quantity,
			//		Price = x.Price,
			//		Remaining = x.Remaining,
			//		Status = x.Status,
			//		AuthorName = x.Author.AuthorName,
			//		CategoryName = x.Category.CategoryName
			//	});
			var matchingBookIds = (
					from role in roles
					where role.ListRole.Split(',').Intersect(currentUserRoles).Any()
					select role.Book.Id).Distinct().ToList();
			var book = from books in _unitOfWork.Book.GetQueryable()
					where matchingBookIds.Contains(books.Id)
					select new BookViewModel
					{
						Id = books.Id,
						BookName = books.BookName,
						Title = books.Title,
						Content = books.Content,
						YearOfRelease = books.YearOfRelease,
						Producer = books.Producer,
						Quantity = books.Quantity,
						Price = books.Price,
						Remaining = books.Remaining,
						Status = books.Status,
						AuthorName = books.Author.AuthorName,
						CategoryName = books.Category.CategoryName
					};
			if (request.Filter != null)
			{
				book = book.Filter(request.Filter);
			}
			if (request.SearchString != null)
			{
				book = book.Search(request.SearchString);
			}

			var result = await book.Pagination(request.PageIndex, request.PageSize, request.Sort);
			return result;
		}
	}
}
