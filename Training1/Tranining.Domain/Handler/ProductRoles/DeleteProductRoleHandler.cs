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
	public class DeleteProductRoleHandler : BaseHandler,IRequestHandler<DeleteProductRoleCommand,bool>
	{
		public DeleteProductRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteProductRoleCommand request,CancellationToken cancellationToken)
		{
			var productRole = await _unitOfWork.ProductRole.GetOne(x => x.Id == request.Id);
			if (productRole == null)
			{
				throw new ArgumentException("Not Found");
			}
			await _unitOfWork.ProductRole.Delete(productRole);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}


