using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.UserRoles;

namespace Tranining.Domain.Handler.UserRoles
{
	public class UpdateUserRoleHandler : UserRoleBaseHandler, IRequestHandler<UpdateUserRoleCommand,bool>
	{
		public UpdateUserRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
		{
			var userRole = await _unitOfWork.UserRole.GetOne(x => x.Id == request.Id);
	
			if (userRole == null)
			{
				throw new ArgumentException("NOT FOUND");
			}
			userRole.UserId = request.UserId;
			userRole.RoleId = request.RoleId;
			
			_unitOfWork.UserRole.Update(userRole);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
