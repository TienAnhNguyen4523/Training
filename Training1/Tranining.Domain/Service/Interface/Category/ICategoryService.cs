using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Service.Interface.Category
{
	public interface ICategoryService
	{
		Task<bool> CreateCategory(CreateCategoryCommand request);
		Task<bool> UpdateCategory(UpdateCategoryCommand request);
		Task<bool> DeleteCategory(DeleteCategoryCommand request);
		Task<PaginationSet<CategoryViewModel>> Listing(ListingCategoryCommand request);
	}
}
