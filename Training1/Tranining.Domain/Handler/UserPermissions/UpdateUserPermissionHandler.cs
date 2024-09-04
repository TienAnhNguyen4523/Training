using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.UserPermissions;

namespace Tranining.Domain.Handler.UserPermissions
{
	public class UpdateUserPermissionHandler : BaseHandler, IRequestHandler<UpdateUserPermissionCommand,bool>
	{
		public UpdateUserPermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdateUserPermissionCommand request,CancellationToken cancellationToken)
		{
			var user = await _unitOfWork.User.GetOne(x => x.Id == request.UserId);
			var userPermission = await _unitOfWork.UserPermission.GetOne(x => x.User.Id == request.UserId);
			if (userPermission != null)
			{
				throw new ArgumentException("Id is not found");
			}
			if (user != null)
			{
				throw new ArgumentException("UserId is not found");
			}
			string[] listPermission = request.ListPermission.Split(',');
			var validPermissions = _unitOfWork.Permission.GetQueryable().Select(x => x.PermissionName).ToList();
			var invalidPermissions = listPermission.Where(x => !validPermissions.Contains(x.Trim())).ToList();
			if (invalidPermissions.Any())
			{
				throw new ArgumentException($"The following permissions are invalid: {string.Join(", ", invalidPermissions)}");
			}
			userPermission.User = user;
			userPermission.ListPermission = request.ListPermission;
			_unitOfWork.UserPermission.Update(userPermission);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
