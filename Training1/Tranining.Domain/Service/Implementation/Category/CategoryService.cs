using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Books;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Service.Interface.Category;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Implementation.Category
{
	public class CategoryService : ICategoryService
	{
		private readonly IMediator _mediator;
		public CategoryService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateCategory(CreateCategoryCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateCategory(UpdateCategoryCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteCategory(DeleteCategoryCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<PaginationSet<CategoryViewModel>> Listing(ListingCategoryCommand request)
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
