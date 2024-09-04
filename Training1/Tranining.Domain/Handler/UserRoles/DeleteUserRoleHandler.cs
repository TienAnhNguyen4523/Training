using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.UserRoles;

namespace Tranining.Domain.Handler.UserRoles
{
	public class DeleteUserRoleHandler : UserRoleBaseHandler,IRequestHandler<DeleteUserRoleCommand,bool>
	{
		public DeleteUserRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
		{
			var userRole = await _unitOfWork.UserRole.GetOne(x => x.Id == request.Id);

			if (userRole == null)
			{
				throw new ArgumentException("NOT FOUND");
			}
			await _unitOfWork.UserRole.Delete(userRole);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
