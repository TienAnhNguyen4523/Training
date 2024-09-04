using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Extensions;

namespace Tranining.Domain.Handler.Categories
{
	public class ListingCategoryHandler : BaseHandler, IRequestHandler<ListingCategoryCommand, PaginationSet<CategoryViewModel>>
	{
		public ListingCategoryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<PaginationSet<CategoryViewModel>> Handle(ListingCategoryCommand request, CancellationToken cancellationToken)
		{

			//var query = _unitOfWork.Category.GetQueryable()
			//	.Select(x => new CategoryViewModel
			//	{
			//		CategoryId = x.Id,
			//		CategoryName = x.CategoryName
			//	});
			var categories = _unitOfWork.Category.GetQueryable();
			var query = from category in categories
						select new CategoryViewModel
						{
							CategoryId = category.Id,
							CategoryName = category.CategoryName
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
