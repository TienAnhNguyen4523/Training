using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Handler.Authors;

namespace Tranining.Domain.Handler.Categories
{
	public class DeleteCategoryHandler : BaseHandler, IRequestHandler<DeleteCategoryCommand, bool>
	{

		public DeleteCategoryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			var CurrentUserId = request.CurrentUserId;
			var roles = request.CurrentRole;
			var listRole = String.Join(",", roles);
			var category = await _unitOfWork.Category.GetOne(x => x.Id == request.Id);
			var book = await _unitOfWork.Book.GetOne(x => x.Category.Id == category.Id);
			if (category == null)
			{
				throw new ArgumentException("Category doesn't exist");
			}
			if (CurrentUserId != category.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			else{
				if (book != null)
				{
					_unitOfWork.Book.Delete(book);
					//_unitOfWork.Category.Delete(category);
				}
				_unitOfWork.Category.Delete(category);
			}		
			return await _unitOfWork.CommitAsync() > 0;
		}

	}
}
