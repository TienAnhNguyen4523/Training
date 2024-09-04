using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Handler.Users;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Extensions;

namespace Tranining.Domain.Handler.Authors
{
	public class ListingAuthorHandler : BaseHandler, IRequestHandler<ListingAuthorCommand, PaginationSet<AuthorViewModel>>
	{
		public ListingAuthorHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<AuthorViewModel>> Handle(ListingAuthorCommand request, CancellationToken cancellationToken)
		{

			//var query = _unitOfWork.Author.GetQueryable()
			//	.Select(x => new AuthorViewModel
			//	{
			//		AuthorId = x.Id,
			//		AuthorName = x.AuthorName
			//	});
			var authors = _unitOfWork.Author.GetQueryable();
			var query = from author in authors
						select new AuthorViewModel
						{
							AuthorId = author.Id,
							AuthorName = author.AuthorName
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
