using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Implementation;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Roles;


namespace Tranining.Domain.Handler.Roles
{
	public class CreateRoleHandler : RoleBaseHandler, IRequestHandler<CreateRoleCommand,bool>
	{
		public CreateRoleHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
		{
			var checkExist = await CheckRoleExist(request.RoleName);
			if (checkExist)
			{
				throw new ArgumentException("Role name already exists");
			}
			var id = Guid.NewGuid();
			var role = new Role
			{
				Id = id,
				RoleName = request.RoleName,
				isActive = true
			};
			_unitOfWork.Role.Insert(role);
			return await _unitOfWork.CommitAsync() > 0 ;
		}
	}
}
