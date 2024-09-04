using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Authors;
using Tranining.Domain.Command.Permissions;

namespace Tranining.Domain.Handler.Permissions
{
	public class DeletePermissionHandler : BaseHandler, IRequestHandler<DeletePermissionCommand,bool>
	{
		public DeletePermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeletePermissionCommand request,CancellationToken cancellationToken)
		{
			var permission = await _unitOfWork.Permission.GetOne(x => x.Id == request.Id);
			if (permission == null)
			{
				throw new ArgumentException("Not Found");
			}
			await _unitOfWork.Permission.Delete(permission);
			return await _unitOfWork.CommitAsync() > 0;

		}
	}
}
