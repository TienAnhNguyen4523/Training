using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Permissions;

namespace Tranining.Domain.Handler.Permissions
{
	public class CreatePermissionHandler : BaseHandler, IRequestHandler<CreatePermissionCommand,bool>
	{
		public CreatePermissionHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreatePermissionCommand request,CancellationToken cancellationToken)
		{
		
			var id = Guid.NewGuid();
			var permission = new Permission 
			{ 
				Id = id,
				PermissionName = request.PermissionName
			};
			_unitOfWork.Permission.Insert(permission);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
