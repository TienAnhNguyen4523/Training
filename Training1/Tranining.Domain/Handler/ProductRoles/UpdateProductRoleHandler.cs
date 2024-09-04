using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.ProductRoles;

namespace Tranining.Domain.Handler.ProductRoles
{
	public class UpdateProductRoleHandler : BaseHandler, IRequestHandler<UpdateProductRoleCommand,bool>
	{
		public UpdateProductRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }	
		public async Task<bool> Handle(UpdateProductRoleCommand request,CancellationToken cancellationToken)
		{
			var ProductRole = await _unitOfWork.ProductRole.GetOne(x => x.Id == request.Id);
			var book = await _unitOfWork.Book.GetOne(x => x.BookName == request.Name);
			if (ProductRole == null)
			{
				throw new ArgumentException("Not Found");
			}
			ProductRole.Book = book;
			ProductRole.ListRole = request.ListRole;
			_unitOfWork.ProductRole.Update(ProductRole);
			return await _unitOfWork.CommitAsync() > 0;

		}
	}
}
