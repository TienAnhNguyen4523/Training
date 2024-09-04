using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Permissions;

namespace Tranining.Domain.Handler.Permissions
{
	public class UpdatePermissionHandler : BaseHandler,IRequestHandler<UpdatePermissionCommand,bool>
	{
		public UpdatePermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
		{
			var permission = await _unitOfWork.Permission.GetOne(x => x.Id == request.Id);
			if (permission == null)
			{
				throw new ArgumentException("Not Found");
			}
			permission.PermissionName = request.Name;
			_unitOfWork.Permission.Update(permission);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
