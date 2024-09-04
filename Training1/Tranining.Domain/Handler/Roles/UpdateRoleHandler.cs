using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Roles;

namespace Tranining.Domain.Handler.Roles
{
	public class UpdateRoleHandler : RoleBaseHandler, IRequestHandler<UpdateRoleCommand, bool>
	{
		public UpdateRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdateRoleCommand request,CancellationToken cancellationToken)
		{
			var roleName = await _unitOfWork.Role.GetOne(x => x.Id == request.Id);
			if (roleName == null)
			{
				throw new ArgumentNullException("Role name doesn't exist");
			}
			else
			{
				roleName.RoleName = request.RoleName;
				roleName.isActive = request.IsActive;
			}
			_unitOfWork.Role.Update(roleName);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
