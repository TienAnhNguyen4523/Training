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
	public class CreateUserPermissionHandler : BaseHandler,IRequestHandler<CreateUserPermissionCommand,bool>
	{
		public CreateUserPermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateUserPermissionCommand request,CancellationToken cancellationToken)
		{
			var currentUserRoles = request.ListRole;
			
			var user = await _unitOfWork.User.GetOne(x => x.Id == request.UserId);
			var roles = _unitOfWork.UserRole.GetQueryable().Where(x => x.UserId == user.Id).Select(x => x.Role.RoleName).ToList();
			foreach( var role in roles )
			{
				if(role == "StandardUser" && request.ListPermission != "view" && roles.Count() == 1)
				{
					throw new ArgumentException("StandardUser only can view");
				}
			}
			var check = await _unitOfWork.UserPermission.GetOne(x => x.User.Id == request.UserId);
			if(check != null)
			{
				throw new ArgumentException("This user has already claimed permissions"); 
			}
			var list = request.ListPermission;
			
			if (request.ListPermission.Contains("update") && request.ListPermission.Contains("delete")&& !request.ListPermission.Contains("view")) {
				list  = request.ListPermission + ",view";
			}
			string[] listPermission = list.Split(',');	
			var validPermissions = _unitOfWork.Permission.GetQueryable().Select(x => x.PermissionName).ToList();
			var invalidPermissions = listPermission.Where(x => !validPermissions.Contains(x.Trim())).ToList();			
			if (invalidPermissions.Any())
			{
				throw new ArgumentException($"The following permissions are invalid: {string.Join(", ", invalidPermissions)}");
			}
			var userPermission = new UserPermission
			{
				Id = Guid.NewGuid(),
				User = user,
				ListPermission = list
			};
			_unitOfWork.UserPermission.Insert(userPermission);
			return await _unitOfWork.CommitAsync() > 0;

		}
	}
}
