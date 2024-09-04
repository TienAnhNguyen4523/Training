using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.UserRoles;
using Tranining.Domain.Handler.Roles;
using Tranining.Domain.Handler.Users;

namespace Tranining.Domain.Handler.UserRoles
{
	public class CreateUserRoleHandler : UserRoleBaseHandler, IRequestHandler<CreateUserRoleCommand,bool>
	{
		public CreateUserRoleHandler(IUnitOfWork unitOfWork): base(unitOfWork) { }
		public async Task<bool> Handle(CreateUserRoleCommand request,CancellationToken cancellationToken)
		{
			var user = await _unitOfWork.User.GetOne(x => x.Id == request.UserId);
			var role = await _unitOfWork.Role.GetOne(x => x.Id == request.RoleId);
			if(user == null || role == null)
			{
				throw new ArgumentException("NOT FOUND");
			}
			var userRole = new UserRole
			{
				UserId = user.Id,
				RoleId = role.Id
				
			};
			_unitOfWork.UserRole.Insert(userRole);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
