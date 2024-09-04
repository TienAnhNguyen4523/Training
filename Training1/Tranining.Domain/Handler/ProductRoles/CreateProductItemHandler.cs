using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.ProductRoles;

namespace Tranining.Domain.Handler.ProductRoles
{
	public class CreateProductItemHandler : BaseHandler,IRequestHandler<CreateProductRoleCommand,bool>
	{
		public CreateProductItemHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateProductRoleCommand request,CancellationToken cancellationToken)
		{
			var book = await _unitOfWork.Book.GetOne(x => x.BookName == request.BookName);
			var Id = new Guid();
			var productRole = new ProductRole
			{
				Id = Id,
				Book = book,
				ListRole = request.ListRole
			};
			_unitOfWork.ProductRole.Insert(productRole);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
