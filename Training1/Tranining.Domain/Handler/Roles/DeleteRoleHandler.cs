using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Roles;


namespace Tranining.Domain.Handler.Roles
{
	public class DeleteRoleHandler : RoleBaseHandler, IRequestHandler<DeleteRoleCommand,bool>
	{
	
		public DeleteRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
		{

			
			var role = await _unitOfWork.Role.GetOne(x => x.Id == request.Id);
			var userRole = await _unitOfWork.UserRole.GetOne(x=> x.RoleId == role.Id);
			if (role == null)
			{
				throw new ArgumentException("Role name doesn't exist");
			}
			_unitOfWork.UserRole.Delete(userRole);
			_unitOfWork.Role.Delete(role);
			return await _unitOfWork.CommitAsync() > 0;
		}

	}
}
