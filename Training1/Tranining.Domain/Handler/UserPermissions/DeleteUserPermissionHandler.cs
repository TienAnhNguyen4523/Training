using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.UserPermissions;

namespace Tranining.Domain.Handler.UserPermissions
{
	public class DeleteUserPermissionHandler : BaseHandler, IRequestHandler<DeleteUserPermissionCommand,bool>
	{
		public DeleteUserPermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteUserPermissionCommand request,CancellationToken cancellationToken)
		{
			var userPermission = await _unitOfWork.UserPermission.GetById(request.Id);
			if (userPermission == null)
			{
				throw new ArgumentException("Not Found");

			}
			await _unitOfWork.UserPermission.Delete(userPermission);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
