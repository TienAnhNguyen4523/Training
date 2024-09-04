using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Categories;

namespace Tranining.Domain.Handler.Categories
{
	public class CreateCategoryHandler : BaseHandler, IRequestHandler<CreateCategoryCommand,bool>
	{
		public CreateCategoryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			var check = await _unitOfWork.Category.CheckExist(x => x.CategoryName == request.Name);
			if (check)
			{
				throw new ArgumentException("Already Exist");
			}
			var id = Guid.NewGuid();
			var category = new Category
			{
				Id = id,
				CategoryName = request.Name

			};
			_unitOfWork.Category.Insert(category);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
