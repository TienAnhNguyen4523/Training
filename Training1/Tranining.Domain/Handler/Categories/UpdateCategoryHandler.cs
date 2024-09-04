using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Handler.Authors;

namespace Tranining.Domain.Handler.Categories
{
	public class UpdateCategoryHandler : BaseHandler, IRequestHandler<UpdateCategoryCommand, bool>
	{
		public UpdateCategoryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var CurrentUserId = request.CurrentUserId;
			var roles = request.CurrentRole;
			var listRole = String.Join(",", roles);
			var category = await _unitOfWork.Category.GetOne(x => x.Id == request.Id);
			if (category == null)
			{
				throw new ArgumentNullException("Category doesn't exist");
			}
			if (CurrentUserId != category.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			else
			{
				category.CategoryName = request.Name;

			}
			_unitOfWork.Category.Update(category);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
